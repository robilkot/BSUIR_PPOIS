#include "CppUnitTest.h"

#include "../Date.cpp"
#include "../Person.cpp"
#include "../Image.cpp"
#include "../Document.cpp"
#include "../Request.cpp"
#include "../VacationRequest.cpp"
#include "../Article.cpp"
#include "../Report.cpp"
#include "../Blueprint.cpp"
#include "../AssemblyBlueprint.cpp"
#include "../ThreeDimensionalBlueprint.cpp"
#include "../ModelRenderer.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace DocumentSystemUnitTest
{
	TEST_CLASS(DateTest)
	{
	public:
		TEST_METHOD(DateToString)
		{
			Date date(2, 3, 2020);
			Assert::AreEqual((string)"02.03.2020", date.toString());
		}
		TEST_METHOD(DateComparison)
		{
			Date date[5];
			date[3] = Date(2, 3, 2020);

			set<Date> testSet;
			for (const auto& d : date)
				testSet.emplace(d);

			Assert::AreEqual((string)"02.03.2020", testSet.begin()->toString());
		}
	};

	TEST_CLASS(PersonTest)
	{
	public:
		TEST_METHOD(PersonToString1)
		{
			Person people;
			Assert::AreEqual((string)"Unknown Unknown Unemployed", people.toString());
		}
		TEST_METHOD(PersonToString2)
		{
			Person people("Tim", "Is", "Cool");
			Assert::AreEqual((string)"Tim Is Cool", people.toString());
		}
		TEST_METHOD(PersonComparison)
		{
			Person people[5];
			people[3].name = "aTest";

			set<Person> testSet;
			for (const auto& person : people)
				testSet.emplace(person);

			Assert::AreEqual((string)"aTest", testSet.begin()->name);
		}
	};

	TEST_CLASS(ImageTest)
	{
	public:
		TEST_METHOD(Image1)
		{
			Image image;
		}
		TEST_METHOD(Image2)
		{
			Image image("testPath", "Description");
		}
		TEST_METHOD(ImageShow)
		{
			Image image("testPath", "Description");
			image.show();
		}
		TEST_METHOD(ImageEmpty)
		{
			Image image;
			Assert::IsTrue(image.empty());
		}

		TEST_METHOD(ImageSetPath)
		{
			Image image;
			image.setPath("test");
			Assert::IsFalse(image.empty());
		}
	};

	TEST_CLASS(DocumentSystemCreation)
	{
	public:
		TEST_METHOD(RequestCreation)
		{

			Request test;
			Assert::AreEqual((string)"untitled", test.getTitle());
		}
		TEST_METHOD(VacationRequestCreation1)
		{
			VacationRequest test;
			Assert::AreEqual((string)"untitled", test.getTitle());
		}
		TEST_METHOD(VacationRequestCreation2)
		{
			VacationRequest test("title", { Date(2,3,2020), Date(6,3,2020) });
			Assert::AreEqual((string)"title", test.getTitle());
		}

		TEST_METHOD(ReportCreation1)
		{
			Report test;
			Assert::AreEqual((string)"untitled", test.getTitle());
		}
		TEST_METHOD(ReportCreation2)
		{
			Report test("title", "Subject", "BSUIR", "text of report");
			Assert::AreEqual((string)"title", test.getTitle());
		}

		TEST_METHOD(ThreeDimensionalBlueprintCreation1)
		{
			ThreeDimensionalBlueprint test;
			Assert::AreEqual((string)"untitled", test.getTitle());
		}
		TEST_METHOD(ThreeDimensionalBlueprintCreation2)
		{
			double origin[3] = { 1,2,3 };
			ThreeDimensionalBlueprint test(origin, "model", "materials");

			Assert::AreEqual((string)"untitled", test.getTitle());
		}
		
	};

	TEST_CLASS(DocumentSystemEdit)
	{
	public:
		TEST_METHOD(DocumentComparison)
		{
			Article testArticle("A");
			Report testReport("B");
			Assert::IsTrue(testArticle < testReport);
		}

		TEST_METHOD(DocumentShowHeader)
		{
			Request application;

			application.showHeader();
		}

		TEST_METHOD(DocumentShowReferences)
		{
			Request application;

			application.showReferences();
		}

		TEST_METHOD(DocumentSetTitle)
		{
			Request application;

			application.setTitle("Test title");

			Assert::AreEqual((string)"Test title", application.getTitle());
		}

		TEST_METHOD(DocumentSign)
		{
			Request application;

			application.sign();

			Assert::IsTrue(application.isSigned());
		}
		TEST_METHOD(DocumentUnsign)
		{
			Request application;

			application.unsign();

			Assert::IsFalse(application.isSigned());
		}

		TEST_METHOD(DocumentAddAuthor)
		{
			Request application;

			Person people("Tim", "Is", "Cool");
			application.addAuthor(people);
			application.addAuthor(people);

			Person people2("Dan", "Is", "Super");
			application.addAuthor(people2);
		}
		TEST_METHOD(DocumentRemoveAuthor)
		{
			Request application;

			Person people("Tim", "Is", "Cool");
			application.addAuthor(people);

			Person people2("Dan", "Is", "Super");
			application.addAuthor(people2);

			application.removeAuthor(people);
		}
		TEST_METHOD(DocumentRemoveNonExistingAuthor)
		{
			Request application;

			application.removeAuthor(Person());
		}
		TEST_METHOD(DocumentClearAuthors)
		{
			Request application;

			Person people("Tim", "Is", "Cool");
			application.addAuthor(people);

			Person people2("Dan", "Is", "Super");
			application.addAuthor(people2);

			application.clearAuthors();
		}

		TEST_METHOD(DocumentAddReference)
		{
			Request application;

			application.addReference("Ein sehr wichtiges Dokument");
			application.addReference("Ein weiteres wichtiges Dokument");
		}
		TEST_METHOD(DocumentRemoveReference)
		{
			Request application;

			application.addReference("Ein sehr wichtiges Dokument");
			application.addReference("Ein weiteres wichtiges Dokument");

			application.removeReference("Ein sehr wichtiges Dokument");
		}
		TEST_METHOD(DocumentRemoveNonExistingReference)
		{
			Request application;

			application.removeReference("Ein sehr wichtiges Dokument");
		}

		TEST_METHOD(ArticleAddParagraph)
		{
			Article test;

			test.addParagraph({ "text", Image() });
		}

		TEST_METHOD(ArticleRemoveParagraph)
		{
			Article test;
			pair<string, Image> par = { "text", Image() };
			
			test.addParagraph(par);
			test.removeParagraph(par);
		}

		TEST_METHOD(AssemblyBlueprintAddItem)
		{
			AssemblyBlueprint test;
			Blueprint bp1, bp2;

			test.addItem(&bp1);
			test.addItem(&bp2);
		}

		TEST_METHOD(AssemblyBlueprintRemoveItem)
		{
			AssemblyBlueprint test;
			Blueprint bp1, bp2;

			test.addItem(&bp1);
			test.addItem(&bp2);

			test.removeItem(&bp1);
		}

		TEST_METHOD(BlueprintAddPage)
		{
			Blueprint application;

			application.addPage(Image());
		}
		TEST_METHOD(BlueprintRemovePage)
		{
			Blueprint application;

			application.removePage(Image());
		}
		TEST_METHOD(BlueprintClearContent)
		{
			Blueprint application;

			application.clearContent();
		}
	};

	TEST_CLASS(ShowTest)
	{
		TEST_METHOD(RequestShow)
		{
			Request test;
			test.show();
		}

		TEST_METHOD(VacationRequestShow)
		{
			VacationRequest test("title", { Date(2,3,2020), Date(6,3,2020) });
			test.show();
		}

		TEST_METHOD(ReportShow)
		{
			Report test;
			test.show();
		}
		TEST_METHOD(ArticleShow)
		{
			Article test;
			test.show();
		}
		TEST_METHOD(ArticleShow2)
		{
			Article test;
			test.addParagraph({"Test paragraph", Image()});

			test.show();
		}
		TEST_METHOD(BlueprintShow)
		{
			Blueprint test;
			test.show();
		}
		TEST_METHOD(AssemblyBlueprintShow)
		{
			AssemblyBlueprint test;
			test.show();
		}
		TEST_METHOD(ThreeDimensionalBlueprintShow)
		{
			ThreeDimensionalBlueprint test;
			test.show();
		}
	};
}