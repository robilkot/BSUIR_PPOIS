import uuid
from view.main_window import Application
from controller.db_repository import DbRepository

if __name__ == '__main__':
    # DbRepository.debug_queries = False
    window = Application()

    # id = uuid.UUID("2b39ba25-73c0-41a0-bff0-d9b9aee45204")
    # repo.delete_student(id)

    # id = uuid.UUID("2fc702af-18bb-486d-b359-dcf8d8ad6377")
    # repo.delete_absence_reason(id)

    # svc.delete_group(uuid.UUID('BA788746-4317-4DBE-9C81-564A8BE000B7'))
    # svc.add_absence_reason('Other', 'Nobody knows the truth...')
