
Ext.Loader.setPath('Posts', Application.Path.Get('Areas/Settings/Views/Posts'));
Ext.Loader.setPath('TimeNorms', Application.Path.Get('Areas/Settings/Views/TimeNorms'));



Ext.define('Staffs.view.Viewport', {
    extend: 'Common.view.Viewport',
    requires: [
        'Common.view.DictionaryGrid'
    ],

    items: [{
        xtype: 'tabpanel',
        //tabPosition: 'left',
        activeTab: 0,
        items: [{
            title: 'Сотрудники',
            xtype: 'CommonDictionaryGrid',
            store: 'Staffs.store.Staffs',

            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'Staffs.store.Staffs',
                dock: 'bottom',
                displayInfo: true
            }],

            features: [{
                ftype: 'filters',
                encode: false,
                // только по локальным данным
                //  иначе будет отправляться запрос на сервер
                local: true,
                autoReload: false,

                filters: [{
                    type: 'numeric',
                    dataIndex: 'Id'
                }, {
                    type: 'string',
                    dataIndex: 'Name'
                }, {
                    type: 'string',
                    dataIndex: 'Surname'
                }, {
                    type: 'string',
                    dataIndex: 'PatronymicName'
                }, {
                    type: 'string',
                    dataIndex: 'TabelNumber'
                }, {
                    type: 'string',
                    dataIndex: 'PostName'
                }, {
                    type: 'string',
                    dataIndex: 'PostTypeName'
                }, {
                    type: 'date',
                    dataIndex: 'StartDate'
                }, {
                    type: 'date',
                    dataIndex: 'EndDate'
                }, {
                    type: 'string',
                    dataIndex: 'GenderName'
                }, {
                    type: 'string',
                    dataIndex: 'RegNormGraphTypeName'
                }, {
                    type: 'string',
                    dataIndex: 'RegNormName'
                }]

            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id',
                width: 50
            }, {
                text: 'Имя',
                dataIndex: 'Name',
                editor: 'textfield',
                width: 150
            }, {
                text: 'Фамилия',
                dataIndex: 'Surname',
                editor: 'textfield',
                width: 150
            }, {
                text: 'Отчество',
                dataIndex: 'PatronymicName',
                editor: 'textfield',
                width: 150
            }, {
                text: 'Таб.№',
                dataIndex: 'TabelNumber',
                editor: 'textfield',
                width: 150
            }, {
                text: 'Должность',
                dataIndex: 'PostName',
                width: 150,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'Name',
                    store: Ext.create('Posts.store.Posts'),
                    name: 'PostId',
                    mode: 'remote'
                }
            }, {
                text: 'Тип должности',
                dataIndex: 'PostTypeName',
                width: 150,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'Name',
                    store: Ext.create('Posts.store.PostTypes'),
                    name: 'PostTypeId',
                    mode: 'remote'
                }
            }, {
                text: 'Дата устройства на работу',
                dataIndex: 'StartDate',
                xtype: 'datecolumn',
                editor: {
                    xtype: 'datefield',
                    format: 'd.m.Y'
                },
                format: 'd.m.Y',
                width: 150
            }, {
                text: 'Дата увольнения',
                dataIndex: 'EndDate',
                xtype: 'datecolumn',
                editor: {
                    xtype: 'datefield',
                    format: 'd.m.Y'
                },

                format: 'd.m.Y',
                width: 150
            }, {
                text: 'Пол',
                dataIndex: 'GenderName',
                width: 150,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'Name',
                    store: Ext.create('Staffs.store.Genders'),
                    name: 'GenderId',
                    mode: 'remote'
                }
            }, {
                text: 'Норма дней в неделю',
                dataIndex: 'RegNormGraphTypeName',
                width: 150,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'Name',
                    store: Ext.create('TimeNorms.store.RegNormGraphTypes'),
                    name: 'RegNormGraphTypeId',
                    mode: 'remote'
                }
            }, {
                text: 'Нормы времени',
                dataIndex: 'RegNormName',
                width: 150,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'Name',
                    store: Ext.create('TimeNorms.store.RegNorms'),
                    name: 'RegNormId',
                    mode: 'remote'
                }
            }]
        }]
    }],

    headerText: 'Сотрудники'

});