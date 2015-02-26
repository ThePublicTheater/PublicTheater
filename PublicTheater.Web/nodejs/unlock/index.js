
var fs = require('fs');
var file = require('file');
var _ = require('underscore');

module.exports.run = function (dir, config) {

    var patterns = _(config.patterns).map(function (str) {
        return new RegExp(str, "ig");
    });

    var matches = [];

    file.walkSync(dir, function (start, dirs, names) {
        var dirMatches = _(names).chain().select(function (name) {
            return _(patterns).any(function (pattern) {
                return !!name.match(pattern);
            });
        }).map(function (fileName) {
            return [start, fileName].join('\\');
        }).value();

        matches.push(dirMatches);
    });

    matches = _(matches).flatten();
    _(matches).each(function (path) {
        fs.chmod(path, 0755);
    });

    console.log(matches);
}