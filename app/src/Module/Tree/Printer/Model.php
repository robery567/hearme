<?php
/**
 * Created by PhpStorm.
 * User: robery567
 * Date: 22/05/2018
 * Time: 19:42
 */

/**
 * Class Module_Tree_Printer_Model
 */
class Module_Tree_Printer_Model {
    /** @var string The data to be rendered */
    protected $dataToRender = '';

    /**
     * @param Module_Tree_Model $tree
     * @return string
     */
    public function render(Module_Tree_Model $tree) {
        $this->dataToRender = '
                <!doctype html>
                <html>
                    <head>
                        <meta charset="utf-8">
                        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
                        <script src="https://code.jquery.com/jquery-2.1.4.min.js"></script>
                        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
                        <style type="text/css">
                        .node {
                            border: 1px solid #0f0;
                            border-radius: 50px;
                            height: 210px;
                            width: 200px;
                            padding: 10px;
                            text-align: center;
                        }
                        .spacer {
                            height: 100px;
                        }
                        .black {
                            border-color: #000;
                        }
                        .red {
                            border-color: #F00;
                        }
                        .leaf {
                            background: #eee2c1;
                        }
                        .top, .bottom {
                        height: 20px;
                        }
                        .left, .right {
                        display: inline-block;
                        }
                        .parent, .root {
                            border: 1px solid;
                            height: 24px;
                            width: 24px;
                            padding: 2px;
                            border-radius: 12px;
                            text-align: center;
                            display: inline-block;
                        }
                        .parent, .root, .glyphicon-remove {
                        color: #999;
                        }
                        .parent {
                            border-color: #ccc;
                            background: #eee;
                        }
                        .root {
                            border-color: #aad;
                            background: #ccf;
                            color: #aad;
                        }
                        </style>
                    </head>
                    <body>
                        <table class="table">
                            <tr>
                        ';

        $this->infixeRender($tree->getRoot());

        $this->dataToRender .= '
                            </tr>
                        </table>
                    </body>
                </html>
                ';

        return $this->dataToRender;
    }

    /**
     * @param Module_Node_Model $node
     */
    protected function infixeRender(Module_Node_Model $node) {
        if ($node->haveChild(Module_Node_Model::POSITION_LEFT)) {
            $this->dataToRender .= '<td><div class="spacer"></div><table class="table"><tr>';

            $this->infixeRender($node->getChild(Module_Node_Model::POSITION_LEFT));

            $this->dataToRender .= '</tr></table></td>';
        }

        $this->dataToRender .= '<td>';

        $this->nodeRender($node);

        $this->dataToRender .= '</td>';

        if ($node->haveChild(Module_Node_Model::POSITION_RIGHT)) {
            $this->dataToRender .= '<td><div class="spacer"></div><table class="table"><tr>';

            $this->infixeRender($node->getChild(Module_Node_Model::POSITION_RIGHT));

            $this->dataToRender .= '</tr></table></td>';
        }
    }

    /**
     * @param Module_Node_Model $node
     */
    protected function nodeRender(Module_Node_Model $node) {
        $this->dataToRender .= '<div class="node'
            . (Module_Node_Model::COLOR_RED === $node->getColor() ? ' red' : ' black')
            . ($node->isLeaf() ? ' leaf' : '')
            . '">';

        if (null !== $node->getParent()) {
            $this->dataToRender .= '<div class="parent">' . $node->getParent()->getId() . '</div>';
        } else {
            $this->dataToRender .= '<div class="root">
                        <span class="glyphicon glyphicon-home"></span>
                        </div>';
        }

        $this->dataToRender .= '<div class="top">#' . $node->getId();

        $userData = $node->getValue();

        $this->dataToRender .= '</div><div class="bottom">';

        $this->dataToRender .= " <ul> 
                                    <li><strong>Username:</strong> {$userData['username']}</li>
                                    <li><strong>Email:</strong> {$userData['email']}</li>
                                    <li><strong>Gender:</strong> {$userData['gender']}</li>
                                 </ul>";

        if (!$node->isLeaf()) {
            $this->dataToRender .= '<div class="left">' .
                ($node->haveChild(Module_Node_Model::POSITION_LEFT) ?
                    $node->getChild(Module_Node_Model::POSITION_LEFT)->getId() : '<span class="glyphicon glyphicon-remove"></span>') .
                '</div>
                <div class="right">>' .
                ($node->haveChild(Module_Node_Model::POSITION_RIGHT) ?
                    $node->getChild(Module_Node_Model::POSITION_RIGHT)->getId() : '<span class="glyphicon glyphicon-remove"></span>') .
                '</div>';
        } else {
            $this->dataToRender .= '<span class="glyphicon glyphicon-leaf"></span>';
        }

        $this->dataToRender .= '</div></div>';
    }
}
