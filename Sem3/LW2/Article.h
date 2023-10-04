#pragma once

#include "Document.h"
#include "Image.h"
#include <iostream>
#include <string>
#include <vector>

using std::vector;
using std::pair;

class Article : public Document {
private:
	string topic;
	vector<pair<string, Image>> paragraphs;

public:
	Article();

	Article(const string& title, const string& topic, const vector<pair<string, Image>>& paragraphs);
	void addParagraph(const pair<string, Image>& par);
	void removeParagraph(const vector<pair<string, Image>>::iterator it);
	void show() const override;
};