
Ext.define('TimeNorms.model.TimeNormOfMonth', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [{
        name: 'Id',
        type: 'int'
    }, {
        name: 'Norm',
        type: 'number'
    }, {
        name: 'RegNormsId',
        type: 'int'
    }, {
        name: 'RegNormsName',
        type: 'string'
    }, {
        name: 'Year',
        type: 'int'
    }, {
        name: 'Month',
        type: 'int'
    }, {
        name: 'MonthName',
        type: 'string'
    }],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/TimeNormsOfMonth/AddTimeNormOfMonth'),
            read: Application.Path.Get('Settings/TimeNormsOfMonth/GetTimeNormOfMonth'),
            update: Application.Path.Get('Settings/TimeNormsOfMonth/Update'),
            destroy: Application.Path.Get('Settings/TimeNormsOfMonth/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});
