var fs = require('fs');
var _ = require('underscore');
var path = require('path');

module.exports.makeTemplateName = function (filePath) {
    var parts = filePath.split('/');
    var dirName = parts[parts.length - 2];
    var fileName = parts[parts.length - 1];

    var templateName = fileName.replace(/\.handlebars/ig, '');

    return templateName;
};


var recursiveGetPath = function (dir, root, exclude) {

    var filePath = [];
    var find = false;
    var files = fs.readdirSync(root + dir);
    if (exclude.indexOf(dir) !== -1)
        return filePath;
    _(files).each(function (file) {
        // get file type
        var type = file.split('.').pop();

        if (!find && type === 'handlebars') {
            var dirName = dir + '/templates.js';
            var fileName = dir + '/*.handlebars';
            filePath.push([dirName, [fileName]]);
            find = true;
        }

        var currentFilePath = dir === '' ? file : dir + '/' + file;
        var stats = fs.statSync(root + currentFilePath);

        if (stats.isDirectory() && exclude.indexOf(file) == -1) { // don't check node_modules folder
            // check sub folder
            var subFolder = recursiveGetPath(currentFilePath, root, exclude);
            filePath = filePath.concat(subFolder);
        }
    }, this);

    return filePath;
}

module.exports.getHandlebarsPaths = function () {
    var dir = arguments[0] || "",
        root = arguments[1] || path.join(__dirname, '..'),
        exclude = arguments[2] || ['node_modules', 'build'];

    return _.object(recursiveGetPath(dir, root + '/', exclude));
};