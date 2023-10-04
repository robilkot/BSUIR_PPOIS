#include "Blueprint.h"

Blueprint::Blueprint()
	: Document("untitled") { };

Blueprint::Blueprint(const string& title, const string& itemName = "Not specified", const Person& revisor = Person(), const Date& revisionDate = Date())
	: Document(title), itemName(itemName), revisor(revisor), revisionDate(revisionDate) {	};

void Blueprint::showBlueprintHeader() const {
	cout << "Item name: " << itemName << '\n';

	cout << "Revised:\n";
	cout << revisor.toString() << '\n';

	cout << "Revision date: " << revisionDate.toString() << '\n';
}
void Blueprint::showContent() const {
	for (const auto& i : images)
		i->show();
}
void Blueprint::show() const {
	showHeader();
	showBlueprintHeader();

	showReferences();
}