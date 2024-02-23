from financial_state_machine import FinancialStateMachine
from repository.file_repository import FileRepository

repository = FileRepository("appstate.pickle")

gm = FinancialStateMachine(repository)

while True:
    gm.move_next(input())
