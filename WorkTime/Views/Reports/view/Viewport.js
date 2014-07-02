
Ext.define('Reports.view.Viewport', {
    extend: 'Common.view.Viewport',
    requires: [
        'Common.view.DictionaryGrid'
    ],

    items: [{
        xtype: 'panel',
        margin: '20 20 20 20',

        defaults: {
            margin: '20 20 20 20',
        },

        items: [{
            xtype: 'datefield',
            itemId: 'DateFromDateField',
            labelWidth: 30,
            format: 'd.m.Y',
            fieldLabel: 'с '
        }, {
            xtype: 'datefield',
            itemId: 'DateToDateField',
            labelWidth: 30,
            format: 'd.m.Y',
            fieldLabel: 'по '
        }]
    }, {
        xtype: 'panel',
        margin: '20 20 20 20',

        defaults: {
            margin: '20 20 20 20',
        },

        items: [{
            xtype: 'button',
            text: 'Сформировать',
            scale: 'large',
            itemId: 'GenerateT13FormButton',
            icon: Application.Path.Get('Content/images/excel5.gif'),
            width: 200
        }, {
            xtype: 'label',
            text: 'формирование табеля формата Т-13'
        }]
    }, {
        xtype: 'panel',
        margin: '20 20 20 20',

        defaults: {
            margin: '20 20 20 20',
        },

        items: [{
            xtype: 'button',
            text: 'Сформировать',
            scale: 'large',
            itemId: 'GenerateInternalTableButton',
            icon: Application.Path.Get('Content/images/excel5.gif'),
            width: 200
        }, {
            xtype: 'label',
            text: 'формирование внутреннего табеля НМРО "ТЭК"'
        }]
    }]

});
