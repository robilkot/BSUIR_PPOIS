import sys
import uvicorn
from fastapi import FastAPI, Request, Depends
from fastapi.staticfiles import StaticFiles

from internal.services_provider import ServiceProvider

sys.path.insert(1, 'D://work//BSUIR//PPOIS//Sem4//LW1//')
from model.entities.bank import *
from model.entities.credit_card import *
from model.entities.card_owner import *
from model.entities.finance_exceptions import *
from routers import banks

if __name__ == '__main__':
    app = FastAPI()
    app.include_router(banks.router)

    # handle the template and static files.
    app.mount("/static", StaticFiles(directory="static"), name="static")

    uvicorn.run(app, host="127.0.0.1", port=5000, log_level="info")
