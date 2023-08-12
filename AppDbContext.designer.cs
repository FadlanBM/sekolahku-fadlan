﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sekolahku_jude
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="db_sekolah-fadlan")]
	public partial class AppDbContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Inserttb_guru(tb_guru instance);
    partial void Updatetb_guru(tb_guru instance);
    partial void Deletetb_guru(tb_guru instance);
    #endregion
		
		public AppDbContextDataContext() : 
				base(global::sekolahku_jude.Properties.Settings.Default.db_sekolah_fadlanConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AppDbContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AppDbContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AppDbContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AppDbContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<tb_guru> tb_gurus
		{
			get
			{
				return this.GetTable<tb_guru>();
			}
		}
		
		public System.Data.Linq.Table<tb_mapel> tb_mapels
		{
			get
			{
				return this.GetTable<tb_mapel>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tb_guru")]
	public partial class tb_guru : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _guru_id;
		
		private string _guru_name;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onguru_idChanging(string value);
    partial void Onguru_idChanged();
    partial void Onguru_nameChanging(string value);
    partial void Onguru_nameChanged();
    #endregion
		
		public tb_guru()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_guru_id", DbType="VarChar(3) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string guru_id
		{
			get
			{
				return this._guru_id;
			}
			set
			{
				if ((this._guru_id != value))
				{
					this.Onguru_idChanging(value);
					this.SendPropertyChanging();
					this._guru_id = value;
					this.SendPropertyChanged("guru_id");
					this.Onguru_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_guru_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string guru_name
		{
			get
			{
				return this._guru_name;
			}
			set
			{
				if ((this._guru_name != value))
				{
					this.Onguru_nameChanging(value);
					this.SendPropertyChanging();
					this._guru_name = value;
					this.SendPropertyChanged("guru_name");
					this.Onguru_nameChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.tb_mapel")]
	public partial class tb_mapel
	{
		
		private string _mapel_id;
		
		private string _mapel_name;
		
		public tb_mapel()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_mapel_id", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string mapel_id
		{
			get
			{
				return this._mapel_id;
			}
			set
			{
				if ((this._mapel_id != value))
				{
					this._mapel_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_mapel_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string mapel_name
		{
			get
			{
				return this._mapel_name;
			}
			set
			{
				if ((this._mapel_name != value))
				{
					this._mapel_name = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
