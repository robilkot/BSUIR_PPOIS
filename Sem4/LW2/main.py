from __future__ import annotations
from controller.db_service import DbService
import uuid


from view.main_window import Application


if __name__ == '__main__':
    db_svc = DbService('DRIVER={SQL Server};SERVER=localhost,1433;DATABASE=Students;UID=SA;PWD=StudentsDb123$')
    DbService.debug_queries = True

    window = Application(db_svc)

    exit()

    # svc.add_group(221702)

    # svc.delete_group(uuid.UUID('BA788746-4317-4DBE-9C81-564A8BE000B7'))
    # svc.add_absence_reason('Other', 'Nobody knows the truth...')
