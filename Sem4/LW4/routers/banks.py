from fastapi import APIRouter, HTTPException
from starlette.requests import Request
from starlette.templating import Jinja2Templates

from internal.services_provider import ServiceProvider

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
async def post_bank(request: Request):
    # TODO
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)


@router.delete('/{name}')
async def delete_bank(request: Request, name: str):
    finances_service.remove_bank(name)

    # todo: redirect to main
    context = {"request": request}
    return templates.TemplateResponse('index.html', context)
