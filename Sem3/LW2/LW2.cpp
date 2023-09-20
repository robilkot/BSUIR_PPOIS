#include <iostream>
#include <string>
#include <vector>
#include <set>
#include <ctime>

using std::string;
using std::vector;
using std::pair;
using std::set;
using std::cout;

class Date {
	uint8_t day = 1;
	uint8_t month = 1;
	uint32_t year = 2023;

public:
	Date(uint8_t day, uint8_t month, uint32_t year = 2023)
		: day(day), month(month), year(year) {};
	
	Date() {
		time_t t = std::time(0);
		auto today = std::localtime(&t);
		year = today->tm_year + 1900;
		month = today->tm_mon;
		day = today->tm_mday;
	}

	string toString() {
		string result;
		
		if (day < 10) result += '0';
		result += std::to_string(day);
		result += '.';

		if (month < 10) result += '0';
		result += std::to_string(month+1);
		result += '.';
		
		result += std::to_string(year);
		return result;
	}

	bool operator < (const Date& other) {
		if (this->year < other.year) return true;
		if (this->year > other.year) return false;

		if (this->month < other.month) return true;
		if (this->month > other.month) return false;

		if (this->day < other.day) return true;
		if (this->day > other.day) return false;
	}
};

class Person {
protected:
	string name;
	string surname;
	string position;

public:
	Person(const string& name = "Name", const string& surname = "Surname", const string& position = "Unknown")
		: name(name), surname(surname), position(position) {};

	string toString() {
		return name + ' ' + surname + ' ' + position;
	}

	bool operator < (const Person& other) const {
		return this->name < other.name;
	}
};

class Image {
	string filePath;
	string description;

	void show() {

	}
};

class Document {
protected:
	string title;
	Date creationDate;
	Date editDate;
	set<Person> authors;
	bool isSigned = false;

	set<Document*> references;
public:
	Document(const string& title = "untitled", const Date& creationDate = { 1,1,2023 })
		: title(title), creationDate(creationDate), editDate(creationDate) {}

	void updateEditDate() {
		editDate = Date();
	}

	void sign() {
		isSigned = true;
		updateEditDate();
	}
	void unsign() {
		isSigned = false;
		updateEditDate();
	}

	void addAuthor(const Person& author) {
		authors.emplace(author);
		updateEditDate();
	}
	void clearAuthors() {
		authors.clear();
		updateEditDate();
	}
	void setTitle(const string& title) {
		this->title = title;
		updateEditDate();
	}

	virtual void show() = 0;
	virtual void save(string filePath) = 0;

	bool operator < (const Document& other) {
		return this->creationDate < other.creationDate;
	}
};

class Application : Document {
private:
	string request;

public:
	void show() override {

	}
	void save(string filePath) override {

	}
};

class VacationApplication : Application {
private:
	set<pair<Date, Date>> vacationDates;

public:
	void show() override {

	}
	void save(string filePath) override {

	}
};

class Report : Document {
private:
	string subject;
	string institution;
	string text;

public:
	void show() override {
	}
	void save(string filePath) override {

	}
};

class Article : Document {
private:
	string topic;
	vector<pair<string, Image>> paragraphs;

public:
	void show() override {

	}
	void save(string filePath) override {

	}
};

class Blueprint : Document {
protected:
	string itemName;
	set<Person> revisor;
	Date revisionDate;

	set<Image*> images;

public:
	void show() override {

	}
	void save(string filePath) override {

	}
};

class AssemblyBlueprint : Blueprint {
protected:
	set<Blueprint*> elements;

public:
	void show() override {

	}
	void save(string filePath) override {

	}
};

enum class ModelRenderer {
	VRay,
	Corona,
	RayTracing
};

class ThreeDimensionalBlueprint : Blueprint {
private:
	long long origin[3] = { 0,0,0 };
	string modelPath;
	string materialsPath;

	ModelRenderer renderer = ModelRenderer::VRay;

public:
};

int main() {
	Date a;
	cout << a.toString();
	return 0;
}