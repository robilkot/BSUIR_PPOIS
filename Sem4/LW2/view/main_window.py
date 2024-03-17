from __future__ import annotations

import math
import tkinter as tk
import uuid
from tkinter import ttk
from tkinter.messagebox import showerror, showwarning, showinfo
from typing import Optional

from controller.db_service import DbService
from controller.search_criteria import SearchCriteria
from model.student import Student
from view.add_absence_window import AddAbsenceWindow
from view.add_student_window import AddStudentWindow
from view.center_window import center_window
from view.delete_students_window import DeleteStudentsWindow
from view.search_student_window import SearchStudentsWindow


class Application:
    def __init__(self, db_svc: DbService):
        self.db_svc: DbService = db_svc
        self.table_absences_current_page = 1
        self.table_absences_page_size = 10
        self.table_students = None
        self.table_absences = None
        self.table_students_pages_count = None
        self.table_absences_pages_count = None
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
            ttk.Button(toolbar, text="Create file", command=None),
            ttk.Button(toolbar, text="Open file", command=None),
            ttk.Button(toolbar, text="Save to file", command=None),
            ttk.Button(toolbar, text="Open MS SQL", command=None),
            ttk.Button(toolbar, text="Save to MS SQL", command=None),
            ttk.Button(toolbar, text="Close data source", command=None),
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
        self.table_students.bind("<<TreeviewSelect>>", self.select_student)

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
            self.update_students_table_label()

        def set_page_size_absences(event):
            self.table_absences_page_size = int(table_absences_combobox.get())
            self.table_absences_current_page = 1
            self.update_absences_data()
            self.update_absences_table_label()

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

    def add_student(self):
        AddStudentWindow(self.main_window, self)

    def add_absence(self):
        if self.selected_student is None:
            tk.messagebox.showinfo(title='Error', message='Please, select student')
        else:
            AddAbsenceWindow(self.main_window, self)

    def select_student(self, event):
        selection = self.table_students.item(self.table_students.focus())
        try:
            self.selected_student = self.db_svc.get_student(uuid.UUID(selection['values'][6]))
        except IndexError:
            print('Student selection discarded')
        finally:
            self.update_absences_data()

    def delete_by_filter(self):
        DeleteStudentsWindow(self.main_window, self)

    def search_by_filter(self):
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

    def update_students_table_label(self):
        self.table_students_pages_count = math.ceil(
            self.db_svc.count_students_amount(self.search_criteria) / self.search_criteria.page_size)
        self.table_students_label.configure(
            text=(f"Page {self.search_criteria.page_number}/{self.table_students_pages_count}."
                  f" Entries on page:"))
        self.table_students_label.update()

    def update_absences_table_label(self):
        self.table_absences_pages_count = math.ceil(
            self.db_svc.count_absences_amount(self.selected_student.id
                                              if self.selected_student is not None
                                              else None) / self.table_absences_page_size)
        self.table_absences_label.configure(
            text=(f"Page {self.table_absences_current_page}/{self.table_absences_pages_count}."
                  f" Entries on page:"))
        self.table_absences_label.update()

    def update_students_data(self):
        for item in self.table_students.get_children():
            self.table_students.delete(item)

        for s in self.db_svc.get_students(self.search_criteria):
            self.table_students.insert("", tk.END, values=(s.name, s.group.number,
                                                           s.absences_sick, s.absences_other,
                                                           s.absences_unjust, s.absences_total, s.id))

        self.update_students_table_label()

    def update_absences_data(self):
        for item in self.table_absences.get_children():
            self.table_absences.delete(item)

        for s in self.db_svc.get_absences(self.selected_student.id if self.selected_student is not None else None,
                                          self.table_absences_current_page, self.table_absences_page_size):
            self.table_absences.insert("", tk.END, values=(s.student.name, s.date, s.reason.name, s.reason.desc, s.id))

        self.update_absences_table_label()
