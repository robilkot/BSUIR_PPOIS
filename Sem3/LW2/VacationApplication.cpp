#include "VacationApplication.h"

VacationApplication::VacationApplication()
	: Application() { };

VacationApplication::VacationApplication(const string& title, pair<Date, Date> vacationDates = {})
	: Application(title, request), vacationDates(vacationDates) {	};

void VacationApplication::show() const {
	showHeader();

	cout << '\n' << request << '\n';

	cout << "Wanted vacation dates:\n";
	cout << vacationDates.first.toString() << " - " << vacationDates.second.toString() << '\n';

	showReferences();
}