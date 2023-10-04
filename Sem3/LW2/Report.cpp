#include "Report.h"

Report::Report()
	: Document("untitled") { };

Report::Report(const string& title, const string& subject = "Not specified", const string& institution = "Not specified", const string& text = "Empty")
	: Document(title), subject(subject), institution(institution), text(text) {	};


void Report::show() const {
	showHeader();

	cout << '\n' << "Subject: " << subject << '\n'
		<< "Institution: " << institution << "\n\n";

	cout << text;

	cout << '\n';

	showReferences();
}