#include "Document.h"

Document::Document(const string& title)
	: title(title), creationDate(Date()), editDate(creationDate) {}

void Document::updateEditDate() {
	editDate = Date();
}

void Document::sign() {
	hasSignature = true;
	updateEditDate();
}
void Document::unsign() {
	hasSignature = false;
	updateEditDate();
}
bool Document::isSigned() const {
	return hasSignature;
}

void Document::addAuthor(const Person& author) {
	authors.emplace(author);
	updateEditDate();
}
void Document::removeAuthor(const Person& author) {
	authors.erase(author);
	updateEditDate();
}
void Document::clearAuthors() {
	authors.clear();
	updateEditDate();
}

void Document::addReference(const string& reference) {
	references.emplace(reference);
	updateEditDate();
}
void Document::removeReference(const string& reference) {
	references.erase(reference);
	updateEditDate();
}

string Document::getTitle() const {
	return title;
}
void Document::setTitle(const string& title) {
	this->title = title;
	updateEditDate();
}

void Document::showHeader() const {
	cout << title << '\n';
	cout << "Created: " << creationDate.toString() << '\n';
	cout << "Edited: " << editDate.toString() << '\n';
	cout << "Author:\n";
	for (const auto& p : authors)
		cout << p.toString() << '\n';
}
void Document::showReferences() const {
	if (!references.empty()) {
		cout << "References:\n";
		for (const auto& p : references)
			cout << p << '\n';
	}
}

bool Document::operator < (const Document& other) const {
	return this->title < other.title;
}