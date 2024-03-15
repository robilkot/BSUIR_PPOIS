import tkinter as tk
from tkinter import ttk
from tkinter.messagebox import showerror, showwarning, showinfo
from controller.db_service import DbService
from controller.search_criteria import SearchCriteria


# todo: inherit from tk window
class MainWindow:
    def __init__(self, db_svc: DbService):
        self.db_svc = db_svc

    def Get(self):
        limits = {
            'sick': [0, 10],
            'other': [0, 5],
            'unjust': [0, 5]
        }

        criteria = SearchCriteria(None, None, limits, 1, 5)

        window = tk.Tk()
        window.geometry("800x400")
        window.minsize(width=500, height=400)
        window.title("Students and absences app")

        window.columnconfigure(index=0, weight=1)
        window.columnconfigure(index=1, weight=1)
        window.rowconfigure(index=1, weight=1)

        toolbar = tk.Frame(window, background='#FFDDCC', height=25)
        toolbar.grid(row=0, column=0, columnspan=2, sticky=tk.EW)

        students_panel = tk.Frame(window, background='blue', width=20, height=20)
        students_panel.grid(row=1, column=0, sticky=tk.NSEW)

        absences_panel = tk.Frame(window, background='red', width=20, height=20)
        absences_panel.grid(row=1, column=1, sticky=tk.NSEW)

        search_button = tk.Button(toolbar, text="Поиск", command=None)
        search_button.grid(row=0, column=0)

        delete_button = tk.Button(toolbar, text="Удаление", command=None)
        delete_button.grid(row=0, column=1)

        # Left side of main screen

        table_students_columns = ("Id", "Name", "Group", "Sick abs.", "Other abs.", "Unjust abs.", "Total abs.")
        table_students = ttk.Treeview(master=students_panel, columns=table_students_columns, show='headings')

        for s in self.db_svc.get_students(criteria):
            table_students.insert("", tk.END,
                                  values=(s.id, s.name, s.group.number,
                                          s.absences_sick, s.absences_other, s.absences_unjust, s.absences_total))

        table_students.pack(fill=tk.BOTH, expand=True)

        # Right side of main screen

        table_absences_columns = ("Id", "Date", "Reason", "Reason description")
        table_absences = ttk.Treeview(master=absences_panel, columns=table_absences_columns, show='headings')

        for s in self.db_svc.get_absences(None, 1, 10):
            table_absences.insert("", tk.END,
                                  values=(s.id, s.date, s.reason.name, s.reason.desc))

        table_absences.pack(fill=tk.BOTH, expand=True)

        return window
