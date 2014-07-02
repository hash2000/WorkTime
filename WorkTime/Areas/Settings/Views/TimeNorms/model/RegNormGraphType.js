

Ext.define('TimeNorms.model.RegNormGraphType', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [{
        name: 'Id',
        type: 'int'
    }, {
        name: 'Name',
        type: 'string'
    }, {
        name: 'DayCount',
        type: 'int'
    }],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/RegNormGraphType/Add'),
            read: Application.Path.Get('Settings/RegNormGraphType/Get'),
            update: Application.Path.Get('Settings/RegNormGraphType/Update'),
            destroy: Application.Path.Get('Settings/RegNormGraphType/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});
