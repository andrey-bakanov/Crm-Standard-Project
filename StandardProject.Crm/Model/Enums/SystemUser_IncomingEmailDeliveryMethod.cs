//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StandardProject.Crm.Model
{
	
	[System.Runtime.Serialization.DataContractAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.1.0.45")]
	public enum SystemUser_IncomingEmailDeliveryMethod
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Microsoft Dynamics 365 для Outlook", 1)]
		Microsoft_Dynamics_365_dlya_Outlook = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("нет", 0)]
		net = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Почтовый ящик для пересылки", 3)]
		Pochtovyj_yaschik_dlya_peresylki = 3,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Синхронизация на стороне сервера или маршрутизатор электронной почты", 2)]
		Sinhronizaciya_na_storone_servera_ili_marshrutizator_elektronnoj_pochty = 2,
	}
}