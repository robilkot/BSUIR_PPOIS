import pyodbc
import uuid

from typing import Optional

from model.absence import Absence
from model.absence_reason import AbsenceReason
from model.student import Student
from model.group import Group
from controller.search_criteria import SearchCriteria


class DbService:
    debug_queries: bool = False

    def __init__(self):
        self.conn = pyodbc.connect('DRIVER={SQL Server};SERVER=localhost,'
                                   '1433;DATABASE=Students;UID=SA;PWD=StudentsDb123$')

    def count_students_amount(self, search_criteria: SearchCriteria) -> int:
        pass

    # todo: implement
    def count_absences_amount(self, search_criteria: SearchCriteria) -> int:
        pass

    def count_absences(self, student: Student):
        cursor = self.conn.cursor()
        query_absences: str = "SELECT dbo.GetAbsences(?, ?)"

        if DbService.debug_queries:
            print(f'{query_absences} (for each absence reason)')

        cursor.execute(query_absences, 'Sick', student.id)
        for row in cursor.fetchall():
            student.absences_sick = 0 if row[0] is None else row[0]

        cursor.execute(query_absences, 'Other', student.id)
        for row in cursor.fetchall():
            student.absences_other = 0 if row[0] is None else row[0]

        cursor.execute(query_absences, 'Unjust cause', student.id)
        for row in cursor.fetchall():
            student.absences_unjust = 0 if row[0] is None else row[0]

        student.absences_total = student.absences_sick + student.absences_unjust + student.absences_other

    def delete_students(self, search_criteria: SearchCriteria) -> int:
        students_to_delete = self.get_students(search_criteria)

        for student in students_to_delete:
            self.delete_student(student.id)

        return len(students_to_delete)

    def delete_student(self, student_id: uuid.UUID):
        cursor = self.conn.cursor()
        query = f'DELETE FROM Students where Id = ?;'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, student_id)
        self.conn.commit()

    def get_students(self, search_criteria: SearchCriteria) -> list[Student]:
        def check_search_criteria(stud: Student) -> bool:
            for term in search_criteria.criteria[0]:
                limit_min = search_criteria.criteria[0][term][0]
                limit_max = search_criteria.criteria[0][term][1]
                match term:
                    case 'sick':
                        if stud.absences_sick > limit_max or stud.absences_sick < limit_min:
                            return False
                    case 'other':
                        if stud.absences_other > limit_max or stud.absences_other < limit_min:
                            return False
                    case 'unjust':
                        if stud.absences_unjust > limit_max or stud.absences_unjust < limit_min:
                            return False
            return True

        students = list[Student]()
        cursor = self.conn.cursor()

        query: str = 'SELECT * FROM Students'

        query_has_where_statement: bool = False

        if search_criteria.group[0] is not None:
            query_has_where_statement = True
            query += f" where GroupId = '{search_criteria.group[0].id}'"

        if search_criteria.name[0] is not None:
            if query_has_where_statement:
                query += f" and"
            else:
                query += f" where"

            query += f" Name = '{search_criteria.name}'"

        if search_criteria.page_size != 0:
            query += (f" ORDER BY Name OFFSET {(search_criteria.page_number[0] - 1) * search_criteria.page_size}"
                      f" ROWS FETCH NEXT {search_criteria.page_size} ROWS ONLY")

        if DbService.debug_queries:
            print(query)

        cursor.execute(query)

        for row in cursor.fetchall():
            student_group = self.get_group(row[2])
            student = Student(row[0], row[1], student_group, 0, 0, 0, 0)
            self.count_absences(student)
            if check_search_criteria(student):
                students.append(student)

        return students

    def get_student(self, student_id: uuid.UUID) -> Student:
        cursor = self.conn.cursor()
        query = 'SELECT * FROM Students WHERE Id = ?'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, student_id)
        row = cursor.fetchone()

        student_group = self.get_group(row[2])
        student = Student(row[0], row[1], student_group, 0, 0, 0, 0)
        self.count_absences(student)

        return student

    def add_student(self, name: str, group: Group):
        cursor = self.conn.cursor()
        query = f'INSERT INTO Students (Id, Name, GroupId) VALUES (NEWID(), ?, ?);'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, name, group.id)
        self.conn.commit()

    def add_group(self, group_number: int) -> None:
        cursor = self.conn.cursor()
        query = f'INSERT INTO Groups (Id, Number) VALUES (NEWID(), ?);'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, group_number)
        self.conn.commit()

    def delete_group(self, group_id: uuid.UUID) -> None:
        cursor = self.conn.cursor()
        query = f'DELETE FROM Groups where Id = ?;'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, group_id)
        self.conn.commit()

    def get_groups(self) -> list[Group]:
        groups = list()
        cursor = self.conn.cursor()
        query = 'SELECT * FROM Groups'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query)

        for row in cursor.fetchall():
            group = Group(row[0], row[1])
            groups.append(group)

        return groups

    def get_group(self, group_id: uuid.UUID) -> Group:
        cursor = self.conn.cursor()
        query = 'SELECT * FROM Groups WHERE Id = ?'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, group_id)
        row = cursor.fetchone()

        group = Group(row[0], row[1])

        return group

    def add_absence_reason(self, reason_name: str, reason_desc: Optional[str]) -> None:
        cursor = self.conn.cursor()
        query = f'INSERT INTO AbsenceReasons (Id, Name, Description) VALUES (NEWID(), ?, ?);'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, reason_name, reason_desc)
        self.conn.commit()

    def delete_absence_reason(self, reason_id: uuid.UUID) -> None:
        cursor = self.conn.cursor()
        query = f'DELETE FROM AbsenceReasons where Id = ?;'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, reason_id)
        self.conn.commit()

    def get_absence_reasons(self) -> list[AbsenceReason]:
        reasons = list()
        cursor = self.conn.cursor()
        query = 'SELECT * FROM AbsenceReasons'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query)

        for row in cursor.fetchall():
            reason = AbsenceReason(row[0], row[1], row[2])
            reasons.append(reason)

        return reasons

    def get_absence_reason(self, reason_id: uuid.UUID) -> AbsenceReason:
        cursor = self.conn.cursor()
        query = 'SELECT * FROM AbsenceReasons WHERE Id = ?'

        if DbService.debug_queries:
            print(query)

        cursor.execute(query, reason_id)
        row = cursor.fetchone()

        reason = AbsenceReason(row[0], row[1], row[2])

        return reason

    def get_absences(self, student_id: Optional[uuid.UUID], page_number: int = 1, page_size: int = 10) -> list[Absence]:
        cursor = self.conn.cursor()
        query = 'SELECT * FROM Absences'

        if student_id is not None:
            query += f" WHERE StudentId = '{student_id}'"

        if page_size != 0:
            query += f" ORDER BY Date OFFSET {(page_number - 1) * page_size} ROWS FETCH NEXT {page_size} ROWS ONLY"

        if DbService.debug_queries:
            print(query)

        cursor.execute(query)

        absences = list[Absence]()

        for row in cursor.fetchall():
            student = self.get_student(row[2])
            reason = self.get_absence_reason(row[3])
            absence = Absence(row[0], row[1], student, reason)

            absences.append(absence)

        return absences
