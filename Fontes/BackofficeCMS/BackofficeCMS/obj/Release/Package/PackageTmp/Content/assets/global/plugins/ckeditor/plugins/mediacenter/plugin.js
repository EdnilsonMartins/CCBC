// Register the plugin within the editor.
CKEDITOR.plugins.add('mediacenter', {

    // Register the icons. They must match command names.
    icons: 'mediacenter',

    // The plugin initialization logic goes inside this method.
    init: function (editor) {

        // Define the editor command that inserts a timestamp.
        editor.addCommand('insertImage', {

            // Define the function that will be fired when the command is executed.
            exec: function (editor) {
                
                var now = new Date();
                
                editt = editor;
                $("#SelArquivo").modal("show");
                // Insert the timestamp into the document.
                //editor.insertHtml('Inserting image here: /File?Id=999' + '<img alt="" src="../File?id=30433" style="height:100px; width:100px" />');
            }
        });

        // Create the toolbar button that executes the above command.
        editor.ui.addButton('MediaCenter', {
            label: 'Insert File',
            command: 'insertImage',
            toolbar: 'insert'
        });
    }
});