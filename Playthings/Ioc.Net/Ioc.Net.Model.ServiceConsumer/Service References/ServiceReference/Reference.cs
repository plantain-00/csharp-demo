﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ioc.Net.Model.ServiceConsumer.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Ioc.Net.Model.Models")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUsers", ReplyAction="http://tempuri.org/IUserService/GetUsersResponse")]
        Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUsers", ReplyAction="http://tempuri.org/IUserService/GetUsersResponse")]
        System.Threading.Tasks.Task<Ioc.Net.Model.ServiceConsumer.ServiceReference.User[]> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Export", ReplyAction="http://tempuri.org/IUserService/ExportResponse")]
        void Export(Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] users);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Export", ReplyAction="http://tempuri.org/IUserService/ExportResponse")]
        System.Threading.Tasks.Task ExportAsync(Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] users);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Import", ReplyAction="http://tempuri.org/IUserService/ImportResponse")]
        Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] Import(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/Import", ReplyAction="http://tempuri.org/IUserService/ImportResponse")]
        System.Threading.Tasks.Task<Ioc.Net.Model.ServiceConsumer.ServiceReference.User[]> ImportAsync(string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AddUser", ReplyAction="http://tempuri.org/IUserService/AddUserResponse")]
        void AddUser(Ioc.Net.Model.ServiceConsumer.ServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AddUser", ReplyAction="http://tempuri.org/IUserService/AddUserResponse")]
        System.Threading.Tasks.Task AddUserAsync(Ioc.Net.Model.ServiceConsumer.ServiceReference.User user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : Ioc.Net.Model.ServiceConsumer.ServiceReference.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<Ioc.Net.Model.ServiceConsumer.ServiceReference.IUserService>, Ioc.Net.Model.ServiceConsumer.ServiceReference.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public System.Threading.Tasks.Task<Ioc.Net.Model.ServiceConsumer.ServiceReference.User[]> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public void Export(Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] users) {
            base.Channel.Export(users);
        }
        
        public System.Threading.Tasks.Task ExportAsync(Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] users) {
            return base.Channel.ExportAsync(users);
        }
        
        public Ioc.Net.Model.ServiceConsumer.ServiceReference.User[] Import(string fileName) {
            return base.Channel.Import(fileName);
        }
        
        public System.Threading.Tasks.Task<Ioc.Net.Model.ServiceConsumer.ServiceReference.User[]> ImportAsync(string fileName) {
            return base.Channel.ImportAsync(fileName);
        }
        
        public void AddUser(Ioc.Net.Model.ServiceConsumer.ServiceReference.User user) {
            base.Channel.AddUser(user);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(Ioc.Net.Model.ServiceConsumer.ServiceReference.User user) {
            return base.Channel.AddUserAsync(user);
        }
    }
}
