module.exports = function (grunt) {
    var parfait = require('./nodejs/parfait');

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        handlebars: {
            compile: {
                options: {
                    amd: true,
                    processName: parfait.makeTemplateName
                }, 
                files: parfait.getHandlebarsPaths()
            }
        },

        watch: {
            syos: {
                files: '../../../HtmlSyos/Main/HtmlSyos.Client/bin/Debug/Adage.HtmlSyos.Client.dll',
                tasks: ['unlock', 'copy:syos'],
                options: {
                    event: ['added', 'changed']
                }
            }
        },

        unlock: {
            dirs: ['amd', 'DLL', 'bin'],
            patterns: ["templates.js", "HtmlSyos.Client", "HtmlSyos.Adapter", "HtmlSyos"]
        },

        copy: {
            syos: {
                files: [
					{
					    nonull: true,
					    src: '../../../HtmlSyos/Working/HtmlSyos.Client/bin/Debug/Adage.HtmlSyos.Client.dll',
					    dest: './DLLs/Adage.HtmlSyos.Client.dll'
					},
					{
					    nonull: true,
					    src: '../../../HtmlSyos/Working/HtmlSyos.Client/bin/Debug/Adage.HtmlSyos.Client.pdb',
					    dest: './DLLs/Adage.HtmlSyos.Client.pdb'
					},
					{
					    nonull: true,
					    src: '../../../HtmlSyos/Working/HtmlSyos.Client/bin/Debug/Adage.HtmlSyos.Client.dll',
					    dest: './bin/Adage.HtmlSyos.Client.dll'
					},
					{
					    nonull: true,
					    src: '../../../HtmlSyos/Working/HtmlSyos.Client/bin/Debug/Adage.HtmlSyos.Client.pdb',
					    dest: './bin/Adage.HtmlSyos.Client.pdb'
					}
                ]
            }
        },
        templates: {
            files: [
                {
                    nonull: true,
                    src:"./amd/syos/templates/templates.js",
                    dest: "//lincoln/c$/Working/MetOperaFamily/Releases/htmlSyos/MetOperaFamily.Website.EPiServer/amd/syos/templates/templates.js"
                }
            ]
        }
    

    });




grunt.loadNpmTasks('grunt-contrib-handlebars');
grunt.loadNpmTasks('grunt-contrib-watch');
grunt.loadNpmTasks('grunt-contrib-copy');

grunt.registerTask('unlock', 'unlock', function () {
    var unlock = require('./nodejs/unlock');
    unlock.run(__dirname, grunt.config('unlock'));
});

grunt.registerTask('default', ['unlock', 'handlebars']);
grunt.registerTask('copySyos', ['unlock', 'copy:syos']);
grunt.registerTask('copyTemplates', ['unlock', 'copy:templates']);

};
