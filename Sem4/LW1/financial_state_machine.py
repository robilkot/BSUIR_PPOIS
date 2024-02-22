from statemachine import State
from statemachine import StateMachine

from typing import Optional

from finances.bank import Bank
from finances.card_owner import CardOwner
from finances.credit_card import CreditCard


class FinancialStateMachine(StateMachine):
    view_banks = State(initial=True)
    view_bank = State()
    create_bank = State()
    delete_bank = State()
    view_card = State()
    create_card = State()
    delete_card = State()
    view_balance = State()
    deposit = State()
    withdraw = State()
    pay = State()
    change_pin = State()
    change_limit = State()
    toggle_block = State()
    exit_ = State(final=True)

    move_next = (
            exit_.from_(view_banks, cond="navigating_backwards")

            | view_banks.from_(view_bank, cond="navigating_backwards")
            | view_bank.from_(view_banks, cond="navigating_by_index")

            | view_bank.from_(view_card, cond="navigating_backwards")
            | view_card.from_(view_bank, cond="navigating_by_index")

            | create_bank.from_(view_banks, cond="navigating_create")
            | view_banks.from_(create_bank)

            | view_bank.from_(create_card)
            | create_card.from_(view_bank, cond="navigating_create")

            | delete_bank.from_(view_bank, cond="navigating_delete")
            | view_banks.from_(delete_bank)

            | view_balance.from_(view_card, cond="navigating_balance")
            | view_card.from_(view_balance)

            | delete_card.from_(view_card, cond="navigating_delete")
            | view_bank.from_(delete_card)

            # todo: implement states below
            | withdraw.from_(view_card, cond="navigating_withdraw")
            | view_card.from_(withdraw)

            | deposit.from_(view_card, cond="navigating_deposit")
            | view_card.from_(deposit)

            | pay.from_(view_card, cond="navigating_pay")
            | view_card.from_(pay)

            | toggle_block.from_(view_card, cond="navigating_toggle_block")
            | view_card.from_(toggle_block)

            | change_limit.from_(view_card, cond="navigating_limit")
            | view_card.from_(change_limit)

            | change_pin.from_(view_card, cond="navigating_pin")
            | view_card.from_(change_pin)
    )

    def __init__(self):
        self.selected_index: Optional[int] = None
        self.selected_bank: Optional[Bank] = None
        self.selected_card: Optional[CreditCard] = None
        self.banks: list[Bank] = [Bank(name="test bank")]

        super().__init__()

    def navigating_backwards(self, input_: str) -> bool:
        return input_ == 'q'

    def navigating_balance(self, input_: str) -> bool:
        return input_ == 'bal'

    def navigating_pay(self, input_: str) -> bool:
        return input_ == 'pay'

    def navigating_withdraw(self, input_: str) -> bool:
        return input_ == 'wdr'

    def navigating_deposit(self, input_: str) -> bool:
        return input_ == 'dep'

    def navigating_pin(self, input_: str) -> bool:
        return input_ == 'pin'

    def navigating_toggle_block(self, input_: str) -> bool:
        return input_ == 'blc'

    def navigating_limit(self, input_: str) -> bool:
        return input_ == 'lim'

    def navigating_create(self, input_: str) -> bool:
        return input_ == 'c'

    def navigating_delete(self, input_: str) -> bool:
        return input_ == 'd'

    def navigating_by_index(self, input_: str) -> bool:
        if input_.isnumeric():
            self.selected_index = int(input_)
            return True
        return False

    def on_enter_view_banks(self):
        self.selected_bank = None
        print("List of all banks:")
        for i in range(len(self.banks)):
            print(f"{i} - {self.banks[i].name}")

    def on_enter_create_bank(self):
        new_bank = Bank(input("Input bank name"))
        self.banks.append(new_bank)
        print("Bank created")
        self.move_next()

    def on_enter_create_card(self):
        card_number = input("Input card number")
        card_owner = CardOwner('Sample name', 'Sample address', None, 'Sample phone')

        try:
            inp: str = input("Input pin")
            while not str.isnumeric(inp):
                input("Input pin")

            card_pin: int = int(inp)

            new_card = CreditCard(card_number, card_owner, card_pin, False)
            self.selected_bank.add_card(new_card)
            print("Card created")
        except ValueError:
            print("Invalid input. Canceling")
        finally:
            self.move_next()

    def on_enter_view_card(self):
        try:
            if self.selected_card is None:
                self.selected_card = self.selected_bank.get_cards()[self.selected_index]

            print("Card information")
            print(self.selected_card.card_number)
            print(self.selected_card.owner.name)
        except IndexError:
            print("Invalid index")
            self.move_next('q')

    def on_enter_view_bank(self):
        self.selected_card = None
        try:
            if self.selected_bank is None:
                self.selected_bank = self.banks[self.selected_index]

            print(f"{self.selected_bank.name} information:")
            cards = self.selected_bank.get_cards()
            for i in range(len(cards)):
                print(f"{i} - {cards[i].card_number}; {cards[i].owner.name}")
        except IndexError:
            print("Invalid index")
            self.move_next('q')

    def on_enter_delete_bank(self):
        print("Deleting bank")
        self.banks.remove(self.selected_bank)
        self.selected_bank = None
        self.move_next()

    def on_enter_delete_card(self):
        print("Deleting card")
        self.selected_bank.remove_card(self.selected_card)
        self.selected_card = None
        self.move_next()

    def on_enter_exit_(self):
        print("Saving state and exiting...")
        # todo: save
        exit()

