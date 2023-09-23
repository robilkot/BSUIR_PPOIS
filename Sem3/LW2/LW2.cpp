#include <iostream>
#include <string>
#include <vector>
#include <set>
#include <ctime>
//#include <fstream>

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

	string toString() const {
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

	bool operator < (const Date& other) const {
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

	string toString() const {
		return name + ' ' + surname + ' ' + position;
	}

	bool operator < (const Person& other) const {
		return this->name < other.name;
	}
};

class Image {
	string filePath;
	string description;
	bool isEmpty;

public:
	bool empty() const {
		return isEmpty;
	}
	void show() const {
		if (!description.empty()) {
			cout << "Image: " << description;
		}
		if (!filePath.empty())
			cout << "\nPath: " << filePath;
		else
			cout << "Image not found";
	}
};

enum class DocType {
	Unknown,
	Application,
	VacationApplication,
	Report,
	Article,
	Blueprint,
	AssemblyBlueprint,
	ThreeDimensionalBlueprint
};

//class FileSystem {
//private:
//	DocType docType;
//
//public:
//	FileSystem(DocType type)
//		: docType(type) { };
//
//	void setType(DocType type) {
//		docType = type;
//	}
//	DocType getType() {
//		return docType;
//	}
//
//	void save(Document* doc, const string& filePath) {
//		std::ofstream file(filePath);
//		if (!file.is_open())
//			throw std::runtime_error("Couldn't save file");
//
//		switch (docType) {
//		case DocType::Application: {
//
//			break;
//		}
//		case DocType::VacationApplication: {
//
//			break;
//		}
//		case DocType::Report: {
//
//			break;
//		}
//		case DocType::Article: {
//
//			break;
//		}
//		case DocType::Blueprint: {
//
//			break;
//		}
//		case DocType::AssemblyBlueprint: {
//
//			break;
//		}
//		case DocType::ThreeDimensionalBlueprint: {
//
//			break;
//		}
//		default: {
//			throw std::invalid_argument("Invalid file type specified");
//		}
//		}
//	}
//};


class Document {
protected:
	const DocType docType;
	string title;
	const Date creationDate;
	Date editDate;
	set<Person> authors;
	bool hasSignature = false;

	set<string> references;

public:
	Document(const string& title = "untitled", DocType docType = DocType::Unknown)
		: title(title), creationDate(Date()), editDate(creationDate), docType(docType) {}

	void updateEditDate() {
		editDate = Date();
	}

	void sign() {
		hasSignature = true;
		updateEditDate();
	}
	void unsign() {
		hasSignature = false;
		updateEditDate();
	}
	bool isSigned() const {
		return hasSignature;
	}

	void addAuthor(const Person& author) {
		authors.emplace(author);
		updateEditDate();
	}
	void removeAuthor(const Person& author) {
		authors.erase(author);
		updateEditDate();
	}
	void clearAuthors() {
		authors.clear();
		updateEditDate();
	}

	void addReference(const string& reference) {
		references.emplace(reference);
	}
	void removeReference(const string& reference) {
		references.erase(reference);
	}

	string getTitle() const {
		return title;
	}
	void setTitle(const string& title) {
		this->title = title;
		updateEditDate();
	}

	virtual void showHeader() const {
		cout << title << '\n';
		cout << "Created: " << creationDate.toString() << '\n';
		cout << "Edited: " << editDate.toString() << '\n';
		cout << "Author:\n";
		for (const auto& p : authors)
			cout << p.toString() << '\n';
	}
	virtual void showReferences() const {
		if (!references.empty()) {
			cout << "References:\n";
			for (const auto& p : references)
				cout << p << '\n';
		}
	}
	virtual void show() const = 0;
	//virtual void saveHeader(const string& filePath) const {
	//	std::ofstream os(filePath, std::ios::out);
	//	if (!os.is_open())
	//		throw std::runtime_error("Couldn't open file");

	//	os << (int)docType << title << creationDate.toString()
	//		<< editDate.toString() << authors <<
	//}
	//virtual void saveReferences(const string& filePath) const {

	//}
	//virtual void save(string filePath) const = 0;

	bool operator < (const Document& other) const {
		return this->creationDate < other.creationDate;
	}
};

class Application : public Document {
protected:
	string request;

public:
	Application()
		: Document("untitled", DocType::Application) { };

	Application(const string& title, const string& request = "Not specified")
		: Document(title, DocType::Application), request(request) {	};

	void show() const override {
		showHeader();

		cout << '\n' << request << '\n';
		
		showReferences();
	}
	//void save(string filePath) const override {	
	//	//FileSystem system(DocType::Application);
	//	//system.save((Document*)this, filePath);
	//}
};

class VacationApplication : public Application {
private:
	set<pair<Date, Date>> vacationDates;

public:
	void show() const override {
		showHeader();

		cout << '\n' << request << '\n';

		if (!vacationDates.empty()) {
			cout << "Wanted vacation dates:\n";
			for (const auto& d : vacationDates)
				cout << d.first.toString() << " - " << d.second.toString() << '\n';
		}

		showReferences();
	}
	/*void save(string filePath) const override {

	}*/
};

class Report : public Document {
private:
	string subject;
	string institution;
	string text;

public:
	void show() const override {
		showHeader();

		cout << '\n' << "Subject: " << subject << '\n'
			<< "Institution: " << institution << "\n\n";

		cout << text;

		cout << '\n';

		showReferences();
	}
	/*void save(string filePath) const override {

	}*/
};

class Article : public Document {
private:
	string topic;
	vector<pair<string, Image>> paragraphs;

public:
	void show() const override {
		showHeader();

		cout << '\n' << "Topic: " << topic << "\n\n";

		for (const auto& p : paragraphs) {
			cout << p.first << '\n';

			if(!p.second.empty())
				p.second.show();

			cout << '\n';
		}

		showReferences();
	}
	/*void save(string filePath) const override {

	}*/
};

class Blueprint : public Document {
protected:
	string itemName;
	set<Person> revisor;
	Date revisionDate;

	set<Image*> images;

public:
	void showBlueprintHeader() const {
		cout << "Item name: " << itemName << '\n';

		cout << "Revised:\n";
		if (revisor.empty())
			cout << "Nobody";
		else
			for (const auto& p : revisor) {
				cout << p.toString() << '\n';
			}
		cout << "Revision date: " << revisionDate.toString() << '\n';
	}
	void showContent() const {
		for (const auto& i : images)
			i->show();
	}
	void show() const override {
		showHeader();
		showBlueprintHeader();

		showReferences();
	}
	/*void save(string filePath) const override {

	}*/
};

class AssemblyBlueprint : public Blueprint {
protected:
	set<Blueprint*> elements;

public:
	void show() const override {
		showHeader();
		showBlueprintHeader();

		for (const auto e : elements)
			e->showContent();

		showReferences();
	}
	/*void save(string filePath) const override {

	}*/
};

enum class ModelRenderer {
	VRay,
	Corona,
	RayTracing
};

class ThreeDimensionalBlueprint : public Blueprint {
private:
	long long origin[3] = { 0,0,0 };
	string modelPath;
	string materialsPath;

	ModelRenderer renderer = ModelRenderer::VRay;

	void renderPreview(ModelRenderer renderer) const {
		switch (renderer) {
		case ModelRenderer::VRay: {
			cout << "--- Some cool render with VRay ---";
			break;
		}
		case ModelRenderer::Corona: {
			cout << "--- Some cool render with Corona ---";
			break;
		}
		case ModelRenderer::RayTracing: {
			cout << "--- Some cool render with RT ---";
			break;
		}
		default: {
			throw std::invalid_argument("No available renderer specified");
		}
		}
	}

public:
	void show() const override {
		showHeader();
		showBlueprintHeader();

		cout << "Path to model: " << modelPath << '\n';
		cout << "Path to materials: " << materialsPath << '\n';
		
		cout << '\n';
		renderPreview(renderer);
		cout << '\n';

		showReferences();
	}
	/*void save(string filePath) const override {

	}*/
};

int main() {
	
	return 0;
}