
Ext.define('TimeNorms.model.PostTypesDayTimeCoefficient', {
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
        name: 'MaleCoefficient',
        type: 'number'
    }, {
        name: 'FemaleCoefficient',
        type: 'number'
    }, {
        name: 'MaleTimeNormOfDay',
        type: 'number'
    }, {
        name: 'FemaleTimeNormOfDay',
        type: 'number'
    }],

    idProperty: 'Id',

    proxy: {
        type: 'rest',
        api: {
            create: Application.Path.Get('Settings/PostTypesDayTimeCoefficient/Add'),
            read: Application.Path.Get('Settings/PostTypesDayTimeCoefficient/GetPostTypesDayTimeCoefficient'),
            update: Application.Path.Get('Settings/PostTypesDayTimeCoefficient/UpdatePostTypesDayTimeCoefficient'),
            destroy: Application.Path.Get('Settings/PostTypesDayTimeCoefficient/Delete')
        },
        reader: 'DefaultReader',
        writer: 'DefaultWriter'
    }

});
