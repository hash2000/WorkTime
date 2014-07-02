
Ext.define('TimeNorms.model.Holiday', {
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
        name: 'StartDate',
        type: 'date'
    }, {
        name: 'EndDate',
        type: 'date'
    }, {
        name: 'IsShiftOfYear',
        type: 'boolean'
    }, {
        name: 'IsHoliday',
        type: 'boolean'
    }, {
        name: 'IsNormsCheck',
        type: 'boolean'
    }, {
        name: 'Description',
        type: 'string'
    }],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/Holidays/Add'),
            read: Application.Path.Get('Settings/Holidays/Get'),
            update: Application.Path.Get('Settings/Holidays/Update'),
            destroy: Application.Path.Get('Settings/Holidays/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});
