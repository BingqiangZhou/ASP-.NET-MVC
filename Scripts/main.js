$(document).ready(
    function () {
        if (getUrlParam("sortType") != null) {
            $("#btnGroupDrop1").text(getUrlParam("sortType"));
            $(".nav-item.nav-link").each(
                function () {
                    if ($(this).text() == getUrlParam("Sort")) {
                        $(this).addClass("disabled");
                        return;
                    }
                }
            )
        }
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
        $(".dropdown-item").click(
            function () {
                $("#btnGroupDrop1").text($(this).text());
                var sort = null;
                $(".nav-item.nav-link").each(
                    function () {
                        if ($(this).hasClass("disabled")) {
                            sort = $(this).text();
                            return;
                        }
                    }
                )
                if (sort!=null)
                    location.href = "?Sort=" + sort + "&sortType=" + $(this).text();
            }
        )
        $(".nav-item.nav-link").click(
            function () {
                var that = this;
                $(".nav-item.nav-link").each(
                    function () {
                        if (this != that)
                            $(this).removeClass("disabled");
                        else
                            $(this).addClass("disabled");
                    }
                )        
            }
        )
        $("#Sort").click(
            function () {
                $(".nav-item.nav-link").each(
                    function () {
                        $(this).removeClass("disabled");
                    }
                )        
            }
        )
        $(".nav-item.nav-link").click(
            function () {
                var sortType = $("#btnGroupDrop1").text();
                location.href = "?Sort=" + $(this).text() + "&sortType=" + sortType;
            }
        )
    }
)