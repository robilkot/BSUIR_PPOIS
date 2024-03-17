from view.main_window import Application
from controller.db_repository import DbRepository

if __name__ == '__main__':
    DbRepository.debug_queries = False

    window = Application()

    # svc.add_group(221702)
    # svc.delete_group(uuid.UUID('BA788746-4317-4DBE-9C81-564A8BE000B7'))
    # svc.add_absence_reason('Other', 'Nobody knows the truth...')
