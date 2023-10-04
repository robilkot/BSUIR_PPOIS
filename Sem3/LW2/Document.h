#pragma once

#include <iostream>
#include <string>
#include <set>
#include "Date.h"
#include "Person.h"

using std::string;
using std::set;
using std::cout;

class Document {
protected:
	string title;
	const Date creationDate;
	Date editDate;
	set<Person> authors;
	bool hasSignature = false;

	set<string> references;

public:
	
	Document(const string& title);
	void updateEditDate();

	void sign();
	void unsign();
	bool isSigned() const;

	void addAuthor(const Person& author);
	void removeAuthor(const Person& author);
	void clearAuthors();

	void addReference(const string& reference);
	void removeReference(const string& reference);

	string getTitle() const;
	void setTitle(const string& title);

	virtual void showHeader() const;
	virtual void showReferences() const;
	virtual void show() const = 0;

	bool operator < (const Document& other) const;
};