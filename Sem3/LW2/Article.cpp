#include "Article.h"

Article::Article()
	: Document("untitled") { };

Article::Article(const string& title, const string& topic = "Not specified", const vector<pair<string, Image>>& paragraphs = { {"Empty", Image()} })
	: Document(title), topic(topic), paragraphs(paragraphs) {	};

void Article::addParagraph(const pair<string, Image>& par) {
	paragraphs.emplace_back(par);
}

void Article::removeParagraph(const vector<pair<string, Image>>::iterator it) {
	paragraphs.erase(it);
}

void Article::show() const {
	showHeader();

	cout << '\n' << "Topic: " << topic << "\n\n";

	for (const auto& p : paragraphs) {
		cout << p.first << '\n';

		if (!p.second.empty())
			p.second.show();

		cout << '\n';
	}

	showReferences();
}
