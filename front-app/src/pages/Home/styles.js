import styled from 'styled-components';

export const Container = styled.div`
    position:absolute;
    left:50%;
    top:50%;
    transform:translate(-50%,-50%);
    height:80%;
    width:40%;
    min-height:200px;
    min-width:300px;
    background-color:#FFF;
    box-shadow: 0 0 1em gray;
    padding:50px;
    .error{
        color:red;
        font-size:12px;
    }
    span{
        font-size:20px;
    }
    .saldoPositivo{
        color:green;
    }
    .saldoNegativo{
        color:red;
    }
    hr{
        margin-top:10px;
        margin-bottom:10px;
    }
    .btnGroup{
        display:flex;
        justify-content:space-between;
        margin-top:10px;
    }
    .extratoDiv{
        background-color:#eee;
        margin:5px;
        padding:5px;
        border-radius:5px;
        display:flex;
        justify-content:space-between;
    }
    .extratoTxt{
        font-size:18px;

    }
`;
