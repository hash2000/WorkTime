


Ext.define('Posts.view.Viewport', {
    extend: 'Common.view.Viewport',
    requires: [
        'Common.view.DictionaryGrid'
    ],

    items: [{
        xtype: 'tabpanel',
        //tabPosition: 'left',
        activeTab: 0,
        items: [{
            title: 'Должности',
            xtype: 'CommonDictionaryGrid',
            store: 'Posts.store.Posts',

            dockedItems: [{
                xtype: 'pagingtoolbar',
                store: 'Posts.store.Posts',
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
                }]

            }],


            columns: [{
                text: 'Id',
                dataIndex: 'Id'
            }, {
                text: 'Наименование',
                dataIndex: 'Name',
                editor: 'textfield',
                width: 300
            }]
        }, {
            title: 'Типы должностей',
            xtype: 'panel',
            items: [{
                xtype: 'CommonDictionaryGrid',
                store: 'Posts.store.PostTypes',

                itemId: 'PostTypesGrid',

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
                    }]

                }],

                dockedItems: [{
                    xtype: 'pagingtoolbar',
                    store: 'Posts.store.PostTypes',
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
                    text: 'Коэффициент',
                    dataIndex: 'CoefficientName',
                    width: 150,
                    editor: {
                        xtype: 'combo',
                        valueField: 'Id',
                        displayField: 'Name',
                        store: Ext.create('TimeNorms.store.PostTypesDayTimeCoefficients'),
                        name: 'CoefficientId',
                        mode: 'remote'
                    }
                }]

            }, {
                xtype: 'grid',
                store: 'Posts.store.VacationTypesInPostType',

                plugins: [
                    Ext.create('Ext.grid.plugin.RowEditing', {
                        pluginId: 'RowEditorPlugin'
                    })
                ],

                itemId: 'VacationTypesGrid',

                dockedItems: [{
                    xtype: 'pagingtoolbar',
                    store: 'Posts.store.VacationTypesInPostType',
                    dock: 'bottom',
                    displayInfo: true
                }],

                columns: [{
                    text: 'Id',
                    dataIndex: 'Id'
                }, {
                    text: 'Наименование',
                    dataIndex: 'Name',
                    width: 300
                }, {
                    text: 'Обозначение',
                    dataIndex: 'Label',
                    width: 100
                }, {
                    text: 'Использется',
                    dataIndex: 'IsUsed',
                    xtype: 'booleancolumn',
                    editor: 'checkbox',
                    width: 300
                }]

            }]
        }]
    }],

    headerText: 'Должности'
});