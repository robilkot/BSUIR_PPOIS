from __future__ import annotations

import math
import tkinter as tk
import uuid
from tkinter import ttk
from tkinter.messagebox import showerror, showwarning, showinfo
from typing import Optional

from controller.file_repository import FileRepository
from controller.search_criteria import SearchCriteria
from model.student import Student
from view.add_absence_window import AddAbsenceWindow
from view.add_student_window import AddStudentWindow
from view.center_window import center_window
from view.delete_students_window import DeleteStudentsWindow
from view.open_ms_sql_window import OpenMsSqlWindow
from view.search_student_window import SearchStudentsWindow


class Application:
    def __init__(self):
        self.repo = None
        self.table_absences_current_page = 1
        self.table_absences_page_size = 10
        self.table_students = None
        self.table_absences = None
        self.table_students_pages_count = 0
        self.table_absences_pages_count = 0
        self.table_students_label = None
        self.table_absences_label = None
        self.selected_student: Optional[Student] = None
        self.search_criteria = SearchCriteria()
        self.delete_criteria = SearchCriteria()

        self.main_window = tk.Tk()
        self.main_window.minsize(width=800, height=400)
        self.main_window.title("Lazy Students")

        center_window(self.main_window, 1200, 600)

        self.main_window.columnconfigure(index=0, weight=2)
        self.main_window.columnconfigure(index=1, weight=1)
        self.main_window.rowconfigure(index=1, weight=1)

        students_panel = ttk.Labelframe(self.main_window, text="Students")
        students_panel.grid(row=1, column=0, sticky=tk.NSEW)
        students_panel.columnconfigure(index=0, weight=1)
        students_panel.rowconfigure(index=0, weight=1)

        absences_panel = ttk.Labelframe(self.main_window, text="Absences")
        absences_panel.grid(row=1, column=1, sticky=tk.NSEW)
        absences_panel.columnconfigure(index=0, weight=1)
        absences_panel.rowconfigure(index=0, weight=1)

        # toolbar
        toolbar = ttk.Frame(self.main_window)
        toolbar.grid(row=0, column=0, columnspan=2, sticky=tk.EW, padx=2, pady=2)

        toolbar_buttons = (
            ttk.Button(toolbar, text="Create file", command=self.create_file),
            ttk.Button(toolbar, text="Open file", command=self.open_file),
            ttk.Button(toolbar, text="Save file", command=self.save_file),
            ttk.Button(toolbar, text="Open MS SQL", command=self.open_ms_sql),
            ttk.Button(toolbar, text="Search by filter", command=self.search_by_filter),
            ttk.Button(toolbar, text="Delete by filter", command=self.delete_by_filter),
            ttk.Button(toolbar, text="Add student", command=self.add_student),
            ttk.Button(toolbar, text="Add absence", command=self.add_absence),
        )
        for index, button in enumerate(toolbar_buttons):
            button.pack(padx=2, pady=0, side=tk.LEFT)

        # Left side of main screen
        table_students_columns = ("Name", "Group", "Sick abs.", "Other abs.", "Unjust abs.", "Total abs.", "Id")
        self.table_students = ttk.Treeview(master=students_panel, columns=table_students_columns, show='headings')
        for id_title, title in enumerate(table_students_columns):
            self.table_students.heading(f"#{id_title + 1}", text=title)
            self.table_students.column(f"#{id_title + 1}", minwidth=len(title) + 15, width=len(title) + 15)

        self.table_students.grid(row=0, column=0, sticky=tk.NSEW, padx=5, pady=5)
        self.table_students.bind("<<TreeviewSelect>>", self._select_student)

        # bottom toolbar
        table_students_buttons = ttk.Frame(students_panel)
        table_students_buttons.grid(row=1, column=0, sticky=tk.EW)

        table_students_next_page_btn = ttk.Button(master=table_students_buttons,
                                                  command=self.next_page_students, text="Next page")
        table_students_next_page_btn.pack(padx=3, pady=2, side=tk.RIGHT)
        table_students_prev_page_btn = ttk.Button(master=table_students_buttons,
                                                  command=self.prev_page_students, text="Prev page")
        table_students_prev_page_btn.pack(padx=3, pady=2, side=tk.RIGHT)

        # Right side of main screen
        table_absences_columns = ("Student", "Date", "Reason", "Reason description", "Id")
        self.table_absences = ttk.Treeview(master=absences_panel, columns=table_absences_columns, show='headings')
        for id_title, title in enumerate(table_absences_columns):
            self.table_absences.heading(f"#{id_title + 1}", text=title)
            self.table_absences.column(f"#{id_title + 1}", minwidth=len(title) + 10, width=len(title) + 10)

        # make desc column wider
        self.table_absences.column("#4", minwidth=80, width=100)

        self.table_absences.grid(row=0, column=0, sticky=tk.NSEW, padx=5, pady=5)

        # bottom toolbar
        table_absences_buttons = ttk.Frame(absences_panel)
        table_absences_buttons.grid(row=1, column=0, sticky=tk.EW)

        table_absences_next_page_btn = ttk.Button(master=table_absences_buttons,
                                                  command=self.next_page_absences, text="Next page")
        table_absences_next_page_btn.pack(padx=3, pady=2, side=tk.RIGHT)
        table_absences_prev_page_btn = ttk.Button(master=table_absences_buttons,
                                                  command=self.prev_page_absences, text="Prev page")
        table_absences_prev_page_btn.pack(padx=3, pady=2, side=tk.RIGHT)

        # page size combobox
        page_count = ["5", "10", "20"]

        def set_page_size_students(event):
            self.table_students_current_page = 1
            self.search_criteria.page_number = self.table_students_current_page
            self.search_criteria.page_size = int(table_students_combobox.get())
            self.update_students_data()

        def set_page_size_absences(event):
            self.table_absences_page_size = int(table_absences_combobox.get())
            self.table_absences_current_page = 1
            self.update_absences_data()

        table_absences_combobox = ttk.Combobox(master=table_absences_buttons, values=page_count, width=3)
        table_absences_combobox.pack(side=tk.RIGHT)
        table_absences_combobox.set(page_count[1])
        table_absences_combobox.bind("<<ComboboxSelected>>", set_page_size_absences)

        table_students_combobox = ttk.Combobox(master=table_students_buttons, values=page_count, width=3)
        table_students_combobox.pack(side=tk.RIGHT)
        table_students_combobox.set(page_count[1])
        table_students_combobox.bind("<<ComboboxSelected>>", set_page_size_students)

        self.table_students_label = ttk.Label(master=table_students_buttons, text="None")
        self.table_students_label.pack(side=tk.RIGHT, padx=5)
        self.table_absences_label = ttk.Label(master=table_absences_buttons, text="None")
        self.table_absences_label.pack(side=tk.RIGHT, padx=5)

        self.update_students_data()
        self.update_absences_data()

        self.main_window.mainloop()

    def create_file(self):
        pass

    def open_file(self):
        self.repo = None
        self.update_students_data()
        self.update_absences_data()
        # todo file
        self.repo = FileRepository()
        self.update_students_data()
        self.update_absences_data()

    def save_file(self):
        pass

    def open_ms_sql(self):
        self.repo = None
        self.update_students_data()
        self.update_absences_data()

        self.main_window.wait_window(OpenMsSqlWindow(self.main_window, self))

        self.update_students_data()
        self.update_absences_data()

    def add_student(self):
        if self.repo is None:
            tk.messagebox.showinfo(title='Error', message='Please, open data source')
        else:
            AddStudentWindow(self.main_window, self)

    def add_absence(self):
        if self.selected_student is None:
            tk.messagebox.showinfo(title='Error', message='Please, select student')
        else:
            AddAbsenceWindow(self.main_window, self)

    def _select_student(self, event):
        selection = self.table_students.item(self.table_students.focus())
        try:
            self.selected_student_id = uuid.UUID(selection['values'][6])
        except IndexError:
            pass
            # print('Student selection discarded')
        finally:
            self.update_absences_data()

    def delete_by_filter(self):
        if self.repo is None:
            tk.messagebox.showinfo(title='Error', message='Please, open data source')
        else:
            DeleteStudentsWindow(self.main_window, self)

    def search_by_filter(self):
        if self.repo is None:
            tk.messagebox.showinfo(title='Error', message='Please, open data source')
        else:
            SearchStudentsWindow(self.main_window, self)

    def next_page_students(self) -> None:
        if self.search_criteria.page_number < self.table_students_pages_count:
            self.search_criteria.page_number = self.search_criteria.page_number + 1
            self.update_students_data()

    def next_page_absences(self) -> None:
        if self.table_absences_current_page < self.table_absences_pages_count:
            self.table_absences_current_page = self.table_absences_current_page + 1
            self.update_absences_data()

    def prev_page_students(self) -> None:
        if self.search_criteria.page_number > 1:
            self.search_criteria.page_number = self.search_criteria.page_number - 1
            self.update_students_data()

    def prev_page_absences(self) -> None:
        if self.table_absences_current_page > 1:
            self.table_absences_current_page = self.table_absences_current_page - 1
            self.update_absences_data()

    def _update_students_table_label(self):
        new_text: str = "No active data source."

        if self.repo is not None:
            self.table_students_pages_count = math.ceil(
                self.repo.count_students_amount(self.search_criteria) / self.search_criteria.page_size)
            new_text = f"Page {self.search_criteria.page_number}/{self.table_students_pages_count}."

        new_text = new_text + " Entries on page: "
        self.table_students_label.configure(text=new_text)
        self.table_students_label.update()

    def _update_absences_table_label(self):
        new_text: str = "No active data source."

        if self.repo is not None:
            self.table_absences_pages_count = math.ceil(
                self.repo.count_absences_amount(self.selected_student.id
                                                if self.selected_student is not None
                                                else None) / self.table_absences_page_size)
            new_text = f"Page {self.table_absences_current_page}/{self.table_absences_pages_count}."

        new_text = new_text + " Entries on page: "
        self.table_absences_label.configure(text=new_text)
        self.table_absences_label.update()

    def update_students_data(self):
        for item in self.table_students.get_children():
            self.table_students.delete(item)

        if self.repo is not None:
            for s in self.repo.get_students(self.search_criteria):
                self.table_students.insert("", tk.END, values=(s.name, s.group.number,
                                                               s.absences_sick, s.absences_other,
                                                               s.absences_unjust, s.absences_total, s.id))

        self._update_students_table_label()

    def update_absences_data(self):
        for item in self.table_absences.get_children():
            self.table_absences.delete(item)

        if self.repo is not None:
            for s in self.repo.get_absences(self.selected_student.id if self.selected_student is not None else None,
                                            self.table_absences_current_page, self.table_absences_page_size):
                self.table_absences.insert("", tk.END,
                                           values=(s.student.name, s.date, s.reason.name, s.reason.desc, s.id))

        self._update_absences_table_label()
