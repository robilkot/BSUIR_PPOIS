#include "CppUnitTest.h"

#include "../Date.cpp"
#include "../Person.cpp"
#include "../Image.cpp"
#include "../Document.cpp"
#include "../Application.cpp"
#include "../VacationApplication.cpp"
#include "../Article.cpp"
#include "../Report.cpp"
#include "../Blueprint.cpp"
#include "../AssemblyBlueprint.cpp"
#include "../ThreeDimensionalBlueprint.cpp"

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
			Person person;
			Assert::AreEqual((string)"Unknown Unknown Unemployed", person.toString());
		}
		TEST_METHOD(PersonToString2)
		{
			Person person("Tim", "Is", "Cool");
			Assert::AreEqual((string)"Tim Is Cool", person.toString());
		}
		TEST_METHOD(PersonComparison)
		{
			Person person[5];
			person[3].name = "aTest";

			set<Person> testSet;
			for (const auto& p : person)
				testSet.emplace(p);

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
		TEST_METHOD(ApplicationCreation)
		{

			Application test;
			Assert::AreEqual((string)"untitled", test.getTitle());
		}
		TEST_METHOD(VacationApplicationCreation1)
		{
			VacationApplication test;
			Assert::AreEqual((string)"untitled", test.getTitle());
		}
		TEST_METHOD(VacationApplicationCreation2)
		{
			VacationApplication test("title", { Date(2,3,2020), Date(6,3,2020) });
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
			Application application;

			application.showHeader();
		}

		TEST_METHOD(DocumentShowReferences)
		{
			Application application;

			application.showReferences();
		}

		TEST_METHOD(DocumentSetTitle)
		{
			Application application;

			application.setTitle("Test title");

			Assert::AreEqual((string)"Test title", application.getTitle());
		}

		TEST_METHOD(DocumentSign)
		{
			Application application;

			application.sign();

			Assert::IsTrue(application.isSigned());
		}
		TEST_METHOD(DocumentUnsign)
		{
			Application application;

			application.unsign();

			Assert::IsFalse(application.isSigned());
		}

		TEST_METHOD(DocumentAddAuthor)
		{
			Application application;

			Person person("Tim", "Is", "Cool");
			application.addAuthor(person);
			application.addAuthor(person);

			Person person2("Dan", "Is", "Super");
			application.addAuthor(person2);
		}
		TEST_METHOD(DocumentRemoveAuthor)
		{
			Application application;

			Person person("Tim", "Is", "Cool");
			application.addAuthor(person);

			Person person2("Dan", "Is", "Super");
			application.addAuthor(person2);

			application.removeAuthor(person);
		}
		TEST_METHOD(DocumentRemoveNonExistingAuthor)
		{
			Application application;

			application.removeAuthor(Person());
		}
		TEST_METHOD(DocumentClearAuthors)
		{
			Application application;

			Person person("Tim", "Is", "Cool");
			application.addAuthor(person);

			Person person2("Dan", "Is", "Super");
			application.addAuthor(person2);

			application.clearAuthors();
		}

		TEST_METHOD(DocumentAddReference)
		{
			Application application;

			application.addReference("Ein sehr wichtiges Dokument");
			application.addReference("Ein weiteres wichtiges Dokument");
		}
		TEST_METHOD(DocumentRemoveReference)
		{
			Application application;

			application.addReference("Ein sehr wichtiges Dokument");
			application.addReference("Ein weiteres wichtiges Dokument");

			application.removeReference("Ein sehr wichtiges Dokument");
		}
		TEST_METHOD(DocumentRemoveNonExistingReference)
		{
			Application application;

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
		TEST_METHOD(ApplicationShow)
		{
			Application test;
			test.show();
		}

		TEST_METHOD(VacationApplicationShow)
		{
			VacationApplication test("title", { Date(2,3,2020), Date(6,3,2020) });
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