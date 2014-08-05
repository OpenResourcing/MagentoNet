
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_X86)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = bin/Debug/MagentoNetService.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

MAGENTONETSERVICE_EXE_MDB_SOURCE=bin/Debug/MagentoNetService.exe.mdb
MAGENTONETSERVICE_EXE_MDB=$(BUILD_DIR)/MagentoNetService.exe.mdb
MAGENTONETSERVICE_EXE_CONFIG_SOURCE=App.config
MYSQL_DATA_DLL_SOURCE=../packages/MySql.Data.6.8.3/lib/net40/MySql.Data.dll
MYSQL_WEB_DLL_SOURCE=../packages/MySql.Web.6.8.3/lib/net40/MySql.Web.dll

endif

ifeq ($(CONFIG),RELEASE_X86)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Release/MagentoNetService.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

MAGENTONETSERVICE_EXE_MDB=
MAGENTONETSERVICE_EXE_CONFIG_SOURCE=App.config
MYSQL_DATA_DLL_SOURCE=../packages/MySql.Data.6.8.3/lib/net40/MySql.Data.dll
MYSQL_WEB_DLL_SOURCE=../packages/MySql.Web.6.8.3/lib/net40/MySql.Web.dll

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MAGENTONETSERVICE_EXE_MDB) \
	$(MAGENTONETSERVICE_EXE_CONFIG) \
	$(MYSQL_DATA_DLL) \
	$(MYSQL_WEB_DLL)  

BINARIES = \
	$(MAGENTONETSERVICE)  


RESGEN=resgen2

MAGENTONETSERVICE_EXE_CONFIG = $(BUILD_DIR)/MagentoNetService.exe.config
MYSQL_DATA_DLL = $(BUILD_DIR)/MySql.Data.dll
MYSQL_WEB_DLL = $(BUILD_DIR)/MySql.Web.dll
MAGENTONETSERVICE = $(BUILD_DIR)/magentonetservice

FILES = \
	Program.cs \
	Properties/AssemblyInfo.cs \
	DataContracts/CatalogCategory.cs \
	DataContracts/CatalogProduct.cs \
	ServiceContracts/Interfaces/ICategoryContract.cs \
	ServiceContracts/Interfaces/IProductContract.cs \
	ServiceContracts/CategoryContract.cs \
	ServiceContracts/ProductContract.cs \
	DataContracts/CategoryContext.cs \
	UnitTests/ServiceTests.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	App.config \
	init-startMagentoNetService.sh \
	init-magentonetservice \
	packages.config \
	MagentoNet-soapui-project.xml \
	App-example.config \
	ServiceContracts \
	DataContracts \
	ServiceContracts/Interfaces \
	UnitTests \
	magentonetservice.in 

REFERENCES =  \
	System \
	System.Core \
	System.Net \
	System.Runtime.Serialization \
	System.ServiceModel \
	System.ServiceModel.Web \
	-pkg:aspnetwebstack \
	System.Data \
	System.Configuration \
	System.Xml \
	System.Data.Linq \
	System.Data.DataSetExtensions \
	System.Data.Services \
	System.Data.Services.Client \
	System.ComponentModel.DataAnnotations \
	-pkg:nunit

DLL_REFERENCES =  \
	../../../../MySql.Data \
	../../../../MySql.Web

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,MAGENTONETSERVICE_EXE_CONFIG))
$(eval $(call emit-deploy-target,MYSQL_DATA_DLL))
$(eval $(call emit-deploy-target,MYSQL_WEB_DLL))
$(eval $(call emit-deploy-wrapper,MAGENTONETSERVICE,magentonetservice,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MAGENTONETSERVICE_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MAGENTONETSERVICE_EXE_CONFIG),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MYSQL_DATA_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MYSQL_WEB_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(bindir)'
	$(call cp,$(MAGENTONETSERVICE),$(DESTDIR)$(bindir))
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MAGENTONETSERVICE_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MAGENTONETSERVICE_EXE_CONFIG),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MYSQL_DATA_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MYSQL_WEB_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MAGENTONETSERVICE),$(DESTDIR)$(bindir))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
