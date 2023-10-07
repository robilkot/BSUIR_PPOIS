#include "VacationRequest.h"

VacationRequest::VacationRequest()
	: Request() { };

VacationRequest::VacationRequest(const string& title, pair<Date, Date> vacationDates = {})
	: Request(title, request), vacationDates(vacationDates) {	};

void VacationRequest::show() const {
	showHeader();

	cout << '\n' << request << '\n';

	cout << "Wanted vacation dates:\n";
	cout << vacationDates.first.toString() << " - " << vacationDates.second.toString() << '\n';

	showReferences();
}