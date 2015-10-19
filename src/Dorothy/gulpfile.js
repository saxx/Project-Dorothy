/// <binding Clean='clean' />

var project = require("./project.json");
var gulp = require('gulp');
var sass = require('gulp-sass');
var concat = require('gulp-concat');
var minifyCss = require('gulp-minify-css');
var rename = require('gulp-rename');
var minifyJs = require('gulp-uglify');
var rimraf = require('rimraf');


var paths = {
    webroot: "./" + project.webroot + "/"
};
paths.js = paths.webroot + "js/**/*.js";
paths.jsDest = paths.webroot + "js/site.min.js";
paths.css = paths.webroot + "css/**/*.scss";
paths.cssDest = paths.webroot + "css/site.min.css";


gulp.task("clean:js", function (cb) {
    rimraf(paths.jsDest, cb);
});
gulp.task("clean:css", function (cb) {
    rimraf(paths.cssDest, cb);
});
gulp.task("clean", ["clean:js", "clean:css"]);


gulp.task('js', ['clean:js'], function () {
    gulp.src([paths.js, '!*.min.js'], { base: '.' })
      .pipe(concat('site.js'))
      .pipe(minifyJs())
      .pipe(rename(paths.jsDest))
      .pipe(gulp.dest('.'));
});


gulp.task('css', ['clean:css'], function () {
    gulp.src(paths.css, { base: '.' })
      .pipe(sass())
      .pipe(concat('site.css'))
      .pipe(minifyCss())
      .pipe(rename(paths.cssDest))
      .pipe(gulp.dest('.'));
});


gulp.task('watch', function () {
    gulp.watch([paths.css, '!*.min.css'], ['css']);
    gulp.watch([paths.js, '!*.min.js'], ['js']);
});

gulp.task('default', ['js', 'css', 'watch']);
