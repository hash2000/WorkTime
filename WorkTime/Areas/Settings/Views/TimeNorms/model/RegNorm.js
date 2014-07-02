
Ext.define('TimeNorms.model.RegNorm', {
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
        name: 'Norm',
        type: 'number'
    }],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/RegNorm/Add'),
            read: Application.Path.Get('Settings/RegNorm/Get'),
            update: Application.Path.Get('Settings/RegNorm/Update'),
            destroy: Application.Path.Get('Settings/RegNorm/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});
