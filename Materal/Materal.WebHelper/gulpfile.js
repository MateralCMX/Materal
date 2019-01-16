var gulp = require("gulp");//gulp主体
var rename = require("gulp-rename");//重命名
var minifyCss = require("gulp-minify-css");//js压缩
var uglify = require("gulp-uglify");//js压缩
var del = require("del");//删除文件
var buildPath = "./build/";
var MateralJsFiles = [
    "./Materal/*.js",
    "./Materal/*/*.js"
];
var MateralResetJsFiles = [
    "./MateralReset/*.js"
];
var MateralResetCssFiles = [
    "./MateralReset/*.css"
];
function renameFunc(file)
{
    file.basename += ".min";
}
gulp.task("clearBuild", function ()
{
    const result = del(buildPath);
    return result;
});
gulp.task("buildMateral", function ()
{
    const result = gulp.src(MateralJsFiles)
        .pipe(uglify())
        .pipe(rename(renameFunc))
        .pipe(gulp.dest(buildPath + "Materal/"));
    return result;
});
gulp.task("buildMateralReset", function ()
{
    gulp.src(MateralResetJsFiles)
        .pipe(uglify())
        .pipe(rename(renameFunc))
        .pipe(gulp.dest(buildPath + "MateralReset/"));
    const result = gulp.src(MateralResetCssFiles)
        .pipe(minifyCss())
        .pipe(rename(renameFunc))
        .pipe(gulp.dest(buildPath + "MateralReset/"));
    return result;
});