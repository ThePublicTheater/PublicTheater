// e  a  s  t  e  r      e  g  g
var over = false;
var arr = new Array();
var time;
var T1 = null;
var start = false;
var str = "";
var l;
var t;
var piece = new Array();
var teller = 0;
var hteller;

function initPuzzle() {
    //Free JavaScript on http://www.ScriptBreaker.com
    var tmpPuzzle = "<div style='vertical-align:bottom;position:relative;height:" + (img_height + 50) + "px;width=" + img_width + "px' valign=bottom><form name=frm_info>";
    tmpPuzzle += "<table><tr><td style='vertical-align:bottom;height:" + (img_height + 50) + "px;width:" + (img_width / 3) + "px'>";

    for (i = 0; i < cols; i++) {
        stop = rows;
        arr[i] = new Array();
        if ((i + 1) == cols) stop--;
        for (j = 0; j < stop; j++) {
            l = Math.round((img_width / cols) * (i));
            t = Math.round((img_height / rows) * (j));
            id = "i_" + i + "j_" + j;
            arr[i][j] = new Array("", l, t, false);
            tmpPuzzle += "<div  style='height:" + Math.round((img_height / rows)) + "px;overflow:hidden;position:absolute;left:" + l + "px;top:" + t + "px;z-index:10;width:" + Math.round((img_width / cols)) + "px' id='" + id + "' onclick=set('" + this.id + "') ><img src='" + img.src + "' style='position:absolute;top:" + (-t) + "px;left:" + (-l) + "px'></div>";
            piece[teller] = new Array(id, l, t, false, teller);
            teller++;
        }
    }

    l = ((img_width / cols) * (cols - 1));
    t = (img_height / rows) * (rows - 1);
    hteller = teller;
    arr[(cols - 1)][(rows - 1)] = new Array("", l, t);
    piece[teller] = new Array("B", 0, 0, false, teller);
    tmpPuzzle += "</td><td style='vertical-align:bottom;height:" + (img_height + 50) + "px;width:" + (img_width / 3) + "px' align=middle><div id=txttimer class=ptext></div></td><td style='vertical-align:bottom;height:" + (img_height + 50) + "px;width:" + (img_width / 3) + "px' align=right><div id=txtgood class=ptext></div></td></tr></table></form></div>";
    teller--;

    document.getElementById("puzzle").innerHTML = tmpPuzzle;
    checker();
}

function reseter() {
    start = true;
    time = new Date();
    arr[(cols - 1)][(rows - 1)] = new Array("", l, t);
    piece[hteller] = new Array("B", 0, 0, false, hteller);
    for (i = 0; i <= teller; i++) piece[i][3] = false;
    changer();
    checker();
    set_time();
}

function set_time() {
   
}

function changer() {
    for (i = 0; i < cols; i++) {
        stop = rows;
        if ((i + 1) == cols) stop--;
        for (j = 0; j < stop; j++) {
            ok = false;
            while (ok == false) {
                check = Math.floor((teller * Math.random()) + 0.5);
                id = "i_" + i + "j_" + j;
                if ((piece[check][3] == false)) {
                    piece[check][3] = true;
                    mob = document.getElementById(id).style;

                    mob.left = piece[check][1] + "px";
                    mob.top = piece[check][2] + "px";
                    arr[i][j][0] = piece[check][0];
                    arr[i][j][3] = get_jpos(piece[check][0], i, j);
                    ok = true;
                }
            }
        }
    }
}

function get_jpos(over, i, j) {
    jpos = over.indexOf("j");
    ipos = parseInt(over.substr(2, (jpos - 2)));
    jpos = parseInt(over.substr((jpos + 2), (over.length - (jpos + 2))));
    if ((i == ipos) && (j == jpos)) return true;
    else return false;
}

function check_blank(i, j) {
    if ((i == (cols - 1)) && (j == (rows - 1))) return true;
    else return false;
}

function set(id) {
    if (!start) {
        reseter();
        return false;
    }
    i = Math.round(parseInt(document.getElementById(id).style.left) / (img_width / cols));
    j = Math.round(parseInt(document.getElementById(id).style.top) / (img_height / rows));
    if (i != (cols - 1) && arr[i + 1][j] && arr[i + 1][j][0] == "") {
        document.getElementById(id).style.left = arr[i + 1][j][1] + "px";
        document.getElementById(id).style.top = arr[i + 1][j][2] + "px";
        arr[i + 1][j][0] = id;
        arr[i][j][0] = "";
        arr[i][j][3] = check_blank(i, j);
        hi = i + 1;
        arr[i + 1][j][3] = get_jpos(id, hi, j);
    }
    if (j != (rows - 1) && arr[i][j + 1] && arr[i][j + 1][0] == "") {
        document.getElementById(id).style.left = arr[i][j + 1][1] + "px";
        document.getElementById(id).style.top = arr[i][j + 1][2] + "px";
        arr[i][j + 1][0] = id;
        arr[i][j][0] = "";
        arr[i][j][3] = check_blank(i, j);
        hj = j + 1;
        arr[i][j + 1][3] = get_jpos(id, i, hj);
    }
    if (i != 0 && arr[i - 1][j] && arr[i - 1][j][0] == "") {
        document.getElementById(id).style.left = arr[i - 1][j][1] + "px";
        document.getElementById(id).style.top = arr[i - 1][j][2] + "px";
        arr[i - 1][j][0] = id;
        arr[i][j][0] = "";
        arr[i][j][3] = check_blank(i, j);
        hi = i - 1;
        arr[i - 1][j][3] = get_jpos(id, hi, j);
    }
    if (j != 0 && arr[i][j - 1] && arr[i][j - 1][0] == "") {
        document.getElementById(id).style.left = arr[i][j - 1][1] + "px";
        document.getElementById(id).style.top = arr[i][j - 1][2] + "px";
        arr[i][j - 1][0] = id;
        arr[i][j][0] = "";
        arr[i][j][3] = check_blank(i, j);
        hj = j - 1;
        arr[i][j - 1][3] = get_jpos(id, i, hj);
    }
    checker();
}

function checker() {
    good = 0;
    further = true;
    for (i = 0; i < cols; i++) {
        for (j = 0; j < rows; j++) {
            if (arr[i][j][3] == false) {
                further = false;
            }
            else good++;
        }
    }
    strgood = good + "/" + (rows * cols);
    if (!start) strgood = (rows * cols) + "/" + (rows * cols);
    document.getElementById("txtgood").innerHTML = strgood;
    if (further) {
        clearTimeout(T1);
        start = false;
        alert("Congratulations! You solved the puzzle!");
    }
}


var img = new Image();
var rows = 3;
var cols = 3;
var img_width = 100;
var img_height = 100;

function ee(info) {
    var retMsg = info.response.toString();

    if (retMsg.lastIndexOf("ee:", 0) === 0) {
        var url = retMsg.substring(3);     

        $('#pzl').append('<img id="pzlimg" />');

        $("#pzlimg")
        .load(function () {
            img_width = $("#pzlimg").width();
            img_height = $("#pzlimg").height();
            img.src = url;
            $("#pzlimg").hide();
            $("#uploadArea").hide();

            initPuzzle();
            reseter();
        })
        .attr('src', url);
    }
}
