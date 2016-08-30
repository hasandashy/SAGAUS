<!-- Alert Message Box start -->
<div class="message">
    <p id="title" class="title28">
        Alert</p>
    <div class="message-box">
        <p id="alertMessage">
        </p>
    </div>
    <form action="#" method="post">
    <input type="button" value="Ok" class="btn-back" style="display:none" id="btnCancel" />
    <input type="button" value="Ok" class="btn-ok" id="btnOk" />&nbsp;
    <div class="clear">
    </div>
    </form>
    <div class="clear">
    </div>
</div>
<script type="text/javascript" language="javascript" >
    $(document).ready(function () {
        $('#btnOk').click(function () {
            if (window.parent.refresh == "y") {
                window.location.reload();
            }
            $('#btnSend').colorbox.close();
            if (window.parent.redirect == "y") {
                parent.sentBack();
            }
        });
        $('#btnCancel').click(function () {
            $('#btnSend').colorbox.close();
            parent.FinalSubmit();
        });
    });
</script>
<!-- Alert Message Box end // -->
