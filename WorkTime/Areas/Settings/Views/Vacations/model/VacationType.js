
Ext.define('Vacations.model.VacationType', {
    extend: 'Ext.data.Model',

    requires: [
        'Common.reader.Default',
        'Common.writer.Default'
    ],

    fields: [
        { name: 'Id', type: 'int' },
        { name: 'Label', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'FullName', type: 'string' }
    ],
    idProperty: 'Id',
    proxy: {
        type: 'rest',
        api: {
            read: Application.Path.Get('Settings/VacationTypes/GetVacationTypes'),
            create: Application.Path.Get('Settings/VacationTypes/Add'),
            update: Application.Path.Get('Settings/VacationTypes/Update'),
            destroy: Application.Path.Get('Settings/VacationTypes/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }
});


