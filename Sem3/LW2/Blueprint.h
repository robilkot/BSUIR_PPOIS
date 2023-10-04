#pragma once

#include "Document.h"
#include "Image.h"
#include "Date.h"
#include <iostream>
#include <string>
#include <set>

class Blueprint : public Document {
protected:
	string itemName;
	Person revisor;
	Date revisionDate;

	set<Image*> images;

public:
	Blueprint();
	Blueprint(const string& title, const string& itemName, const Person& revisor, const Date& revisionDate);
	void showBlueprintHeader() const;
	void showContent() const;
	void show() const override;
};