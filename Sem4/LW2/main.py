from __future__ import annotations
from controller.db_service import DbService
import uuid


from view.main_window import MainWindow


if __name__ == '__main__':
    db_svc = DbService()
    DbService.debug_queries = False

    window = MainWindow(db_svc).Get()
    window.mainloop()

    exit()

    # svc.add_group(221702)
    # print(svc.get_group(uuid.UUID('1AA953DE-8FEF-4C0F-8B0E-D79D36B6DEBC')))
    # for g in svc.get_groups():
    #     print(g)

    # group = svc.get_group(uuid.UUID('1AA953DE-8FEF-4C0F-8B0E-D79D36B6DEBC'))
    # svc.add_student(input('inp name'), group)

    # limits = {
    #     'sick': [0, 2],
    #     'other': [0, 2],
    #     'unjust': [0, 2]
    # }

    # criteria = SearchCriteria(group, None, limits, 1, 5)
    #
    # for a in svc.get_absences(uuid.UUID('977177E2-BF5D-4021-B071-6C01417106C4'), 1, 3):
    #     print(a)

    # svc.delete_group(uuid.UUID('BA788746-4317-4DBE-9C81-564A8BE000B7'))
    # svc.add_absence_reason('Other', 'Nobody knows the truth...')

    # for r in svc.get_absence_reasons():
    #     print(r)
