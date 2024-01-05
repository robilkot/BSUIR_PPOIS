#include <memory>

#include "sc-memory/sc_memory.hpp"


#include "sc-memory/sc_event.hpp"




#define iSearchAgent_hpp_15_init  bool _InitInternal(ScAddr const & outputStructure = ScAddr::Empty) override \
{ \
    ScMemoryContext ctx(sc_access_lvl_make_min, "iSearchAgent::_InitInternal"); \
    ScSystemIdentifierFiver fiver; \
    bool result = true; \
    return result; \
}


#define iSearchAgent_hpp_15_initStatic static bool _InitStaticInternal(ScAddr const & outputStructure = ScAddr::Empty)  \
{ \
    ScMemoryContext ctx(sc_access_lvl_make_min, "iSearchAgent::_InitStaticInternal"); \
    ScSystemIdentifierFiver fiver; \
    bool result = true; \
    return result; \
}


#define iSearchAgent_hpp_15_decl \
private:\
	typedef ScAgent Super;\
protected: \
	iSearchAgent(char const * name, sc_uint8 accessLvl) : Super(name, accessLvl) {}\
	virtual sc_result Run(ScAddr const & listenAddr, ScAddr const & edgeAddr, ScAddr const & otherAddr) override; \
private:\
	static std::unique_ptr<ScEvent> ms_event;\
    static std::unique_ptr<ScMemoryContext> ms_context;\
public: \
	static bool handler_emit(ScAddr const & addr, ScAddr const & edgeAddr, ScAddr const & otherAddr)\
	{\
		iSearchAgent Instance("iSearchAgent", sc_access_lvl_make_min);\
		return Instance.Run(addr, edgeAddr, otherAddr) == SC_RESULT_OK;\
	}\
	static void RegisterHandler()\
	{\
		ms_context.reset(new ScMemoryContext(sc_access_lvl_make_min, "handler_iSearchAgent"));\
		ms_event.reset(new ScEvent(*ms_context, Keynodes::question_isearch, ScEvent::Type::AddOutputEdge, &iSearchAgent::handler_emit));\
        if (ms_event.get())\
        {\
            SC_LOG_INFO("Register agent iSearchAgent");\
        }\
        else\
        {\
            SC_LOG_ERROR("Can't register agent iSearchAgent");\
        }\
	}\
	static void UnregisterHandler()\
	{\
		ms_event.reset();\
		ms_context.reset();\
	}

#define iSearchAgent_hpp_iSearchAgent_impl \
std::unique_ptr<ScEvent> iSearchAgent::ms_event;\
std::unique_ptr<ScMemoryContext> iSearchAgent::ms_context;

#undef ScFileID
#define ScFileID iSearchAgent_hpp

