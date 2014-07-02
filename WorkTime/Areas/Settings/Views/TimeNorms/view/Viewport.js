
Ext.define('TimeNorms.view.Viewport', {
    extend: 'Common.view.Viewport',
    requires: [
        'Common.view.DictionaryGrid'
    ],

    items: [{
        xtype: 'tabpanel',
        //tabPosition: 'left',
        items: [{
            title: 'Нормы времени (Неделя)',
            xtype: 'CommonDictionaryGrid',
            store: 'TimeNorms.store.RegNorms',

            itemId: 'RegNormsGrid',
            
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'TimeNorms.store.RegNorms',
                dock: 'bottom',
                displayInfo: true
            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id'
            }, {
                text: 'Наименование',
                dataIndex: 'Name',
                editor: 'textfield',
                width: 300
            }, {
                text: 'Норма',
                dataIndex: 'Norm',
                editor: 'textfield',
                width: 300
            }]
        }, {
            title: 'Нормы дней (Неделя)',
            xtype: 'CommonDictionaryGrid',
            store: 'TimeNorms.store.RegNormGraphTypes',
            
            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'TimeNorms.store.RegNormGraphTypes',
                dock: 'bottom',
                displayInfo: true
            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id'
            }, {
                text: 'Наименование',
                dataIndex: 'Name',
                editor: 'textfield',
                width: 300
            }, {
                text: 'Количество дней',
                dataIndex: 'DayCount',
                editor: 'textfield',
                width: 300
            }]
        }, {
            title: 'Праздники',
            xtype: 'CommonDictionaryGrid',
            store: 'TimeNorms.store.Holidays',

            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'TimeNorms.store.Holidays',
                dock: 'bottom',
                displayInfo: true
            }],

            itemId: 'HolidaysGrid',

            features: [{
                ftype: 'filters',
                encode: false,
                // только по локальным данным
                //  иначе будет отправляться запрос на сервер
                local: true,

                filters: [{
                    type: 'numeric',
                    dataIndex: 'Id'
                }, {
                    type: 'string',
                    dataIndex: 'Name'
                }, {
                    type: 'date',
                    dataIndex: 'StartDate'
                }, {
                    type: 'date',
                    dataIndex: 'EndDate'
                }, {
                    type: 'string',
                    dataIndex: 'Description'
                }]

            }],

            tbar: [{
                fieldLabel: 'Год',
                xtype: 'combobox',
                itemId: 'HolidaysYearCombo',
                editable: false,
                labelWidth: 36,
                displayField: 'Value'
            }, {
                xtype: 'button',
                itemId: 'HolidaysShiftOnNextYear',
                text: 'Перенести на следующий год'
            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id',
                locked: true
            }, {
                text: 'Наименование',
                dataIndex: 'Name',
                editor: 'textfield',
                width: 500,
                locked: true
            }, {
                text: 'Начало',
                xtype: 'datecolumn',
                dataIndex: 'StartDate',
                format: 'd.m.Y',
                editor: {
                    xtype: 'datefield',
                    format: 'd.m.Y'
                }
            }, {
                text: 'Окончание',
                xtype: 'datecolumn',
                dataIndex: 'EndDate',
                format: 'd.m.Y',
                editor: {
                    xtype: 'datefield',
                    format: 'd.m.Y'
                },
            }, {
                text: 'Переносится (год)',
                dataIndex: 'IsShiftOfYear',
                editor: 'checkbox'
            }, {
                text: 'Праздничный',
                dataIndex: 'IsHoliday',
                editor: 'checkbox'
            }, {
                text: 'Входит в норму времени',
                dataIndex: 'IsNormsCheck',
                editor: 'checkbox'
            }, {
                text: 'Описание',
                dataIndex: 'Description',
                editor: 'textfield',
                width: 500
            }]
        }, {
            title: 'Коэффициенты рабочего времени',
            xtype: 'CommonDictionaryGrid',
            store: 'TimeNorms.store.PostTypesDayTimeCoefficients',

            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'TimeNorms.store.PostTypesDayTimeCoefficients',
                dock: 'bottom',
                displayInfo: true
            }],

            itemId: 'PostTypesDayTimeCoefficientsGrid',

            features: [{
                ftype: 'filters',
                encode: false,
                // только по локальным данным
                //  иначе будет отправляться запрос на сервер
                local: true,

                filters: [{
                    type: 'numeric',
                    dataIndex: 'Id'
                }, {
                    type: 'string',
                    dataIndex: 'Name'
                }, {
                    type: 'numeric',
                    dataIndex: 'Value'
                }]

            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id',
                locked: true
            }, {
                text: 'Наименование',
                dataIndex: 'Name',
                editor: 'textfield',
                width: 500,
                locked: true
            }, {
                text: 'Коеф (Мужской)',
                dataIndex: 'MaleCoefficient',
                editor: 'textfield',
                width: 80
            }, {
                text: 'Коеф (Женский)',
                dataIndex: 'FemaleCoefficient',
                editor: 'textfield',
                width: 80
            }, {
                text: 'Часов в день (Мужской)',
                dataIndex: 'MaleTimeNormOfDay',
                editor: 'textfield',
                width: 80
            }, {
                text: 'Часов в день (Женский)',
                dataIndex: 'FemaleTimeNormOfDay',
                editor: 'textfield',
                width: 80
            }]
        }, {
            title: 'Нормы времени (по месяцам)',
            xtype: 'CommonDictionaryGrid',
            store: 'TimeNorms.store.TimeNormsOfMonth',

            itemId: 'TimeNormsOfMonthGrid',

            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'TimeNorms.store.TimeNormsOfMonth', 
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
                    type: 'numeric',
                    dataIndex: 'Norm'
                }, {
                    type: 'string',
                    dataIndex: 'RegNormsName'
                }, {
                    type: 'string',
                    dataIndex: 'MonthName'
                }]

            }],

            tbar: [{
                fieldLabel: 'Год',
                xtype: 'combobox',
                itemId: 'TimeNormsOfMonthYearCombo',
                labelWidth: 36,
                displayField: 'Value'
            }, {
                xtype: 'button',
                itemId: 'TimeNormsOfMonthShiftOnNextYear',
                text: 'Перенести на следующий год'
            }],

            columns: [{
                text: 'Id',
                dataIndex: 'Id'
            }, {
                text: 'Норма времени',
                dataIndex: 'Norm',
                editor: 'textfield',
                width: 300
            }, {
                text: 'для недельной нормы',
                dataIndex: 'RegNormsName',
                //xtype: 'templatecolumn',
               // tpl: '{RegNormsName}',
                width: 300,
                editor: {
                    xtype: 'combo',
                    valueField: 'Id',
                    displayField: 'Name',
                    store: Ext.create('TimeNorms.store.RegNorms'),
                    name: 'RegNormsId',
                    mode: 'remote'
                }
            }, {
                text: 'Месяц',
                dataIndex: 'MonthName',
                //xtype: 'templatecolumn',
                //tpl: '{MonthName}',
                width: 300,
                editor: {
                    xtype: 'combo',
                    valueField: 'Value',
                    displayField: 'Name',
                    store: Ext.create('Common.store.MonthList', {
                        valueFrom: 1,
                        valueTo: 13
                    }),
                    name: 'Month',
                    mode: 'remote'
                },
                filter: {
                    type: 'string'
                }
            }]

        }]
    }],

    headerText: 'Нормы времени',

    initComponent: function () {
        this.callParent(this);

        var date = new Date();
        var currentyear = date.getFullYear();
        var store = Ext.create('Common.store.FlatList', {
            valueFrom: currentyear - 10,
            valueTo: currentyear + 3,
            valueStep: 1,
            autoLoad: true
        });

        var holidaysyearcombo = this.down('#HolidaysYearCombo');
        holidaysyearcombo.bindStore(store);
        holidaysyearcombo.select(currentyear);

        var tnomyearcombo = this.down('#TimeNormsOfMonthYearCombo');
        tnomyearcombo.bindStore(store);
        tnomyearcombo.select(currentyear);

        var tnomgrid = this.down('#TimeNormsOfMonthGrid');
        var tnomgridstore = tnomgrid.getStore();
        tnomgridstore.modelDefaults.Year = currentyear;
        tnomgridstore.filters.clear();
        tnomgridstore.filter('Year', currentyear);

        var hligrid = this.down('#HolidaysGrid');
        var hligridstore = hligrid.getStore();
        hligridstore.filters.clear();
        hligridstore.filter('StartDate.Year', currentyear);

    }

});