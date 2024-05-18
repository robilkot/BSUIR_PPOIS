from fastapi import APIRouter, HTTPException
from starlette.requests import Request
from starlette.templating import Jinja2Templates

from internal.services_provider import ServiceProvider
from models.bank import Bank
from models.credit_card import CreditCard

router = APIRouter(
    prefix="/banks",
    tags=["banks"],
    responses={404: {"description": "Not found"}},
)

templates = Jinja2Templates(directory="templates")
service_provider = ServiceProvider()
finances_service = service_provider.FinancesService


@router.get("/{name}")
async def view_bank(request: Request, name: str):
    try:
        bank = finances_service.get_bank(name)
    except ValueError as e:
        print("Not found")

    # todo: template
    context = {"request": request,
               "name": name}
    return templates.TemplateResponse("item.html", context)


@router.get('/')
async def view_banks(request: Request):
    try:
        banks = finances_service.get_banks()
    except Exception as e:
        print(f"get_banks threw {str(e)}")

    # todo: template
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)


@router.post('/')
async def post_bank(request: Request, bank: Bank):
    try:
        finances_service.create_bank(bank.name)
    except Exception as e:
        print(f"post_bank threw {str(e)}")

    # todo: template
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)


@router.delete('/{name}')
async def delete_bank(request: Request, name: str):
    finances_service.remove_bank(name)

    # todo: redirect to main
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)


# REGION CARDS
@router.get('/{bank_name}/cards')
async def view_cards(request: Request, bank_name: str):
    try:
        cards = finances_service.get_cards(bank_name)
    except Exception as e:
        print(f"get_cards threw {str(e)}")

    # todo: template
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)


@router.get("/{bank_name}/cards/{card_number}")
async def view_bank(request: Request, bank_name: str, card_number: int):
    try:
        card = finances_service.get_card(bank_name, card_number)
    except Exception as e:
        print(f"get_cards threw {str(e)}")

    # todo: template
    context = {"request": request,
               "name": bank_name}
    return templates.TemplateResponse("item.html", context)


@router.post('/{bank_name}/cards/')
async def post_card(request: Request, bank_name: str, card: CreditCard):
    try:
        finances_service.create_card(bank_name, card.card_number, card.owner, card.pin)
    except Exception as e:
        print(f"create_card threw {str(e)}")

    # todo: template
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)


@router.delete('/{bank_name}/cards/{card_number}')
async def post_card(request: Request, bank_name: str, card_number: int):
    try:
        finances_service.remove_card(bank_name, card_number)
    except Exception as e:
        print(f"remove_card threw {str(e)}")

    # todo: template
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)


# def withdraw(self, bank_name: str, card_number: int, amount: int, card_pin: int)
# def get_balance(self, bank_name: str, card_number: int, card_pin: int) -> int:
# def deposit(self, bank_name: str, card_number: int, amount: int) -> None:
# def pay(self, bank_name: str, card_number: int, amount: int, card_pin: int) -> None:
# def set_limit(self, bank_name: str, card_number: int, amount: int, card_pin: int) -> None:
# def set_pin(self, bank_name: str, card_number: int, new_pin: int) -> None:
# def transfer(self, bank_name: str, sender_card_number: int, receiver_card_number: int,
# def toggle_block(self, bank_name: str, card_number: int) -> None:
