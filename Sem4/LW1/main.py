from financial_state_machine import FinancialStateMachine

gm = FinancialStateMachine()

while True:
    gm.move_next(input())
