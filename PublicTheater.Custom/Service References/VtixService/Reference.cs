﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PublicTheater.Custom.VtixService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="VtixService.VTIXSoap")]
    public interface VTIXSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/InsertVTIXEntry", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool InsertVTIXEntry(string sessionKey, int lineType, string ipAddress, string inputName, string inputAddress, string emailAddress);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface VTIXSoapChannel : PublicTheater.Custom.VtixService.VTIXSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VTIXSoapClient : System.ServiceModel.ClientBase<PublicTheater.Custom.VtixService.VTIXSoap>, PublicTheater.Custom.VtixService.VTIXSoap {
        
        public VTIXSoapClient() {
        }
        
        public VTIXSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VTIXSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VTIXSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VTIXSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool InsertVTIXEntry(string sessionKey, int lineType, string ipAddress, string inputName, string inputAddress, string emailAddress) {
            return base.Channel.InsertVTIXEntry(sessionKey, lineType, ipAddress, inputName, inputAddress, emailAddress);
        }
    }
}