
Ext.Loader.setPath('Staffs', Application.Path.Get('Areas/Settings/Views/Staffs'));

Ext.define('Vacations.view.Viewport', {
    extend: 'Common.view.Viewport',
    requires: [
        'Common.view.DictionaryGrid'
    ],

    items: [{
        xtype: 'tabpanel',
        items: [{
            title: 'Отпуска сотрудников',
            xtype: 'CommonDictionaryGrid',
            store: 'Vacations.store.Vacations',

            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'Vacations.store.Vacations',
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
                    type: 'date',
                    dataIndex: 'StartDate'
                }, {
                    type: 'date',
                    dataIndex: 'EndDate'
                }, {
                    type: 'string',
                    dataIndex: 'StaffName'
                }, {
                    type: 'string',
                    dataIndex: 'VacationTypeName'
                }, {
                    type: 'date',
                    dataIndex: 'EndDateWithHolidays'
                }]

            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id'
            }, {
                text: 'Начало',
                dataIndex: 'StartDate',
                xtype: 'datecolumn',
                format: 'd.m.Y',
                editor: {
                    xtype: 'datefield',
                    format: 'd.m.Y'
                },
                width: 100
            }, {
                text: 'Окончание',
                dataIndex: 'EndDate',
                xtype: 'datecolumn',
                format: 'd.m.Y',
                editor: {
                    xtype: 'datefield',
                    format: 'd.m.Y'
                },
                width: 100
            }, {
                text: 'Окончание (с учетом праздников)',
                dataIndex: 'EndDateWithHolidays',
                xtype: 'datecolumn',
                format: 'd.m.Y',
            }, {
                text: 'Сотрудник',
                dataIndex: 'StaffName',
                //xtype: 'templatecolumn',
                //tpl: '{StaffName}',
                width: 300,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'FullName',
                    store: Ext.create('Staffs.store.Staffs'),
                    name: 'StaffId',
                    mode: 'remote'
                }

            }, {
                text: 'Отпуск',
                dataIndex: 'VacationTypeName',
                //xtype: 'templatecolumn',
                //tpl: '{VacationTypeName}',
                width: 400,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'FullName',
                    store: Ext.create('Vacations.store.VacationTypes'),
                    name: 'VacationTypeId',
                    mode: 'remote'
                }
            }]
        }, {
            title: 'Обозначения',
            xtype: 'CommonDictionaryGrid',
            store: 'Vacations.store.VacationTypes',

            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'Vacations.store.VacationTypes',
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
                    dataIndex: 'Label'
                }, {
                    type: 'string',
                    dataIndex: 'Name'
                }]

            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id',
                width: 50
            }, {
                text: 'Обозначение',
                dataIndex: 'Label',
                editor: 'textfield'
            }, {
                text: 'Наименование',
                dataIndex: 'Name',
                editor: 'textfield',
                width: 500
            }]

        }]
    }],

    headerText: 'Отпуска'

});