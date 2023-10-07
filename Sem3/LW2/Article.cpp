#include "Article.h"

Article::Article()
	: Document("untitled") { };

Article::Article(const string& title, const string& topic = "Not specified", const vector<pair<string, Image>>& paragraphs = { {"Empty", Image()} })
	: Document(title), topic(topic), paragraphs(paragraphs) {	};

void Article::addParagraph(const pair<string, Image>& par) {
	paragraphs.emplace_back(par);
	updateEditDate();
}

void Article::removeParagraph(const pair<string, Image>& par) {
	auto it = std::find(paragraphs.begin(), paragraphs.end(), par);
	paragraphs.erase(it);
	updateEditDate();
}

void Article::show() const {
	showHeader();

	cout << '\n' << "Topic: " << topic << "\n\n";

	for (const auto& par : paragraphs) {
		cout << par.first << '\n';

		if (!par.second.empty())
			par.second.show();

		cout << '\n';
	}

	showReferences();
}
