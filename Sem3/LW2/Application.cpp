#include "Application.h"

Application::Application()
	: Document("untitled") { };

Application::Application(const string& title, const string& request = "Not specified")
	: Document(title), request(request) {	};

void Application::show() const {
	showHeader();

	cout << '\n' << request << '\n';

	showReferences();
}