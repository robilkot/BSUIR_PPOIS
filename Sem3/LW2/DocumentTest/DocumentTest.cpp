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

//namespace Microsoft {
//	namespace VisualStudio {
//		namespace CppUnitTestFramework {
//			template <> static std::wstring ToString(const CantorSet& set)
//			{
//				string converted = set.toString();
//				return std::wstring(converted.begin(), converted.end());
//			}
//		}
//	}
//}

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

	};

	TEST_CLASS(DocumentSystemEdit)
	{
	public:
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

		TEST_METHOD(DocumentClearAuthors)
		{
			Application application;

			Person person("Tim", "Is", "Cool");
			application.addAuthor(person);

			Person person2("Dan", "Is", "Super");
			application.addAuthor(person2);

			application.clearAuthors();
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
	};
}