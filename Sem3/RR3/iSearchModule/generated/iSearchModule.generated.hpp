#include <memory>

#include "sc-memory/sc_memory.hpp"


#include "sc-memory/sc_event.hpp"




#define iSearchModule_hpp_11_init  bool _InitInternal(ScAddr const & outputStructure = ScAddr::Empty) override \
{ \
    ScMemoryContext ctx(sc_access_lvl_make_min, "iSearchModule::_InitInternal"); \
    ScSystemIdentifierFiver fiver; \
    bool result = true; \
    return result; \
}


#define iSearchModule_hpp_11_initStatic static bool _InitStaticInternal(ScAddr const & outputStructure = ScAddr::Empty)  \
{ \
    ScMemoryContext ctx(sc_access_lvl_make_min, "iSearchModule::_InitStaticInternal"); \
    ScSystemIdentifierFiver fiver; \
    bool result = true; \
    return result; \
}


#define iSearchModule_hpp_11_decl \
public:\
	sc_result InitializeGenerated()\
	{\
		if (!ScKeynodes::Init())\
			return SC_RESULT_ERROR;\
		if (!ScAgentInit(false))\
			return SC_RESULT_ERROR;\
		return InitializeImpl();\
	}\
	sc_result ShutdownGenerated()\
	{\
		return ShutdownImpl();\
	}\
	sc_uint32 GetLoadPriorityGenerated()\
	{\
		return 50;\
	}

#define iSearchModule_hpp_iSearchModule_impl 

#undef ScFileID
#define ScFileID iSearchModule_hpp

