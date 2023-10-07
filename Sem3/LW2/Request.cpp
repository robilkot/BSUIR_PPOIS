#include "Request.h"

Request::Request()
	: Document("untitled") { };

Request::Request(const string& title, const string& request = "Not specified")
	: Document(title), request(request) {	};

void Request::show() const {
	showHeader();

	cout << '\n' << request << '\n';

	showReferences();
}